using Apps.PhraseStrings.Model.InteroperableXliff;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Filters.Coders;
using Blackbird.Filters.Constants;
using Blackbird.Filters.Enums;
using Blackbird.Filters.Extensions;
using Blackbird.Filters.Transformations;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;

namespace Apps.PhraseStrings.Actions;

[ActionList("Interoperable XLIFF")]
public class InteroperableXliffActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : PhraseStringsInvocable(invocationContext)
{
    private const string MetadataCategory = "phrase-strings";
    private const string ProjectIdMeta = "project-id";
    private const string BranchMeta = "branch";
    private const string SourceLocaleIdMeta = "source-locale-id";
    private const string SourceLocaleCodeMeta = "source-locale-code";
    private const string TargetLocaleIdMeta = "target-locale-id";
    private const string TargetLocaleCodeMeta = "target-locale-code";
    private const string JobIdMeta = "job-id";

    [Action("Download keys", Description = "Download selected keys for downstream translation or review.")]
    public async Task<DownloadKeysResponse> DownloadKeys(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] DownloadKeysRequest input)
    {
        if (string.IsNullOrWhiteSpace(input.TargetLocale))
            throw new PluginMisconfigurationException("Target locale ISO code or ID is required.");

        var projectDetails = await GetProject(project.ProjectId);
        var locales = await GetProjectLocales(project.ProjectId, input.Branch);
        CreateJobResponse? job = null;
        var branch = input.Branch;

        if (!string.IsNullOrWhiteSpace(input.JobId))
        {
            job = await GetJob(project.ProjectId, input.JobId!, input.Branch);
            branch = string.IsNullOrWhiteSpace(branch) ? job.Branch?.Name : branch;
        }

        var targetLocale = ResolveLocale(locales, input.TargetLocale, "target");
        var sourceLocale = ResolveSourceLocale(locales, input.SourceLocale, job);

        var selectedKeys = await ResolveKeys(project.ProjectId, branch, input.KeyIds, input.KeyNames, job);
        var sourceTranslations = await GetTranslationsByLocale(project.ProjectId, sourceLocale.Id, branch);
        var targetTranslations = await GetTranslationsByLocale(project.ProjectId, targetLocale.Id, branch);

        var sourceByKeyId = sourceTranslations
            .Where(translation => !string.IsNullOrWhiteSpace(translation.Key?.Id))
            .GroupBy(translation => translation.Key!.Id)
            .ToDictionary(group => group.Key, group => group.First(), StringComparer.Ordinal);
        var targetByKeyId = targetTranslations
            .Where(translation => !string.IsNullOrWhiteSpace(translation.Key?.Id))
            .GroupBy(translation => translation.Key!.Id)
            .ToDictionary(group => group.Key, group => group.First(), StringComparer.Ordinal);
        var qualityByTranslationId = await GetTranslationQualityScores(
            project.ProjectId,
            selectedKeys
                .Select(key => targetByKeyId.GetValueOrDefault(key.Id)?.Id)
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Select(id => id!)
                .Distinct(StringComparer.Ordinal));

        var transformation = CreateTransformation(project.ProjectId, sourceLocale, targetLocale, branch, job);
        var coder = new PlaintextCoder();
        var keyIdsByUnit = new Dictionary<UnitGrouping, string>();
        var response = new DownloadKeysResponse
        {
            SourceLocaleId = sourceLocale.Id,
            TargetLocaleId = targetLocale.Id
        };

        foreach (var key in selectedKeys)
        {
            sourceByKeyId.TryGetValue(key.Id, out var sourceTranslation);
            targetByKeyId.TryGetValue(key.Id, out var targetTranslation);

            var unit = CreateUnit(coder, key, sourceTranslation, targetTranslation);
            if (!string.IsNullOrWhiteSpace(targetTranslation?.Id) &&
                qualityByTranslationId.TryGetValue(targetTranslation.Id, out var quality))
            {
                unit.Quality.Score = quality.Score;
                unit.Quality.ProfileReference = string.IsNullOrWhiteSpace(quality.Engine)
                    ? "Phrase Strings"
                    : $"Phrase Strings ({quality.Engine})";
            }

            transformation.Children.Add(unit);
            keyIdsByUnit[unit] = key.Id;

            response.TotalKeysDownloaded++;
            switch (unit.Segments.First().State ?? SegmentState.Initial)
            {
                case SegmentState.Initial:
                    response.TotalKeysWithoutTargetTranslations++;
                    break;
                case SegmentState.Translated:
                    response.TotalKeysWithUnverifiedTargetTranslations++;
                    break;
                case SegmentState.Reviewed:
                    response.TotalKeysWithVerifiedTargetTranslations++;
                    break;
                case SegmentState.Final:
                    response.TotalKeysWithReviewedTargetTranslations++;
                    response.TotalKeysWithVerifiedTargetTranslations++;
                    break;
            }
        }

        var xliff = transformation.Serialize(unit => keyIdsByUnit.GetValueOrDefault(unit));
        response.Content = await fileManagementClient.UploadAsync(
            xliff.ToStream(),
            MediaTypes.Xliff2,
            CreateDownloadFileName(projectDetails, targetLocale, job));

        return response;
    }

    [Action("Upload keys", Description = "Upload translated key content and update target translations.")]
    public async Task<UploadKeysResponse> UploadKeys([ActionParameter] UploadKeysRequest input)
    {
        if (input.Content is null)
            throw new PluginMisconfigurationException("Content is required.");

        var stream = await fileManagementClient.DownloadAsync(input.Content);
        var transformationResult = Transformation.Load(stream, input.Content.Name, input.Content.ContentType);

        if (!transformationResult.Success || transformationResult.Value is null)
            throw new PluginMisconfigurationException($"Could not parse XLIFF file: {transformationResult.Error}");

        var transformation = transformationResult.Value;
        var projectId = FirstNotEmpty(input.ProjectId, GetMetadata(transformation, ProjectIdMeta))
            ?? throw new PluginMisconfigurationException("Project ID is missing from input and XLIFF metadata.");
        var sourceLocaleId = GetMetadata(transformation, SourceLocaleIdMeta) ?? string.Empty;
        var targetLocaleId = FirstNotEmpty(input.TargetLocaleId, GetMetadata(transformation, TargetLocaleIdMeta))
            ?? throw new PluginMisconfigurationException("Target locale ID is missing from input and XLIFF metadata.");
        var branch = FirstNotEmpty(input.Branch, GetMetadata(transformation, BranchMeta));
        var jobId = GetMetadata(transformation, JobIdMeta);
        var statesToApply = (input.StatesToApplyToTargetTranslations ?? [SegmentState.Reviewed.Serialize(), SegmentState.Final.Serialize()])
            .Select(state => state.ToSegmentState())
            .Where(state => state.HasValue)
            .Select(state => state!.Value)
            .ToHashSet();

        var uploadedKeyIds = new HashSet<string>(StringComparer.Ordinal);

        foreach (var unit in transformation.GetUnits())
        {
            var segment = unit.Segments.OrderBy(segment => segment.Order).FirstOrDefault();
            if (segment is null)
                continue;

            var target = segment.GetTarget();
            if (string.IsNullOrEmpty(target))
                continue;

            var keyId = unit.Id
                ?? throw new PluginMisconfigurationException("A unit is missing Phrase key ID in unit ID.");
            var translationId = segment.Id;
            var state = segment.State ?? SegmentState.Initial;

            if (string.IsNullOrWhiteSpace(translationId))
            {
                await CreateTranslation(projectId, targetLocaleId, keyId, target, branch, state, statesToApply);
            }
            else
            {
                await UpdateTranslation(projectId, translationId, target, branch, state, statesToApply);
            }

            uploadedKeyIds.Add(keyId);
        }

        if (!string.IsNullOrWhiteSpace(input.Tags) && uploadedKeyIds.Count > 0)
            await AddTags(projectId, targetLocaleId, branch, uploadedKeyIds, input.Tags!);

        return new UploadKeysResponse
        {
            KeysUploaded = uploadedKeyIds.Count,
            ProjectId = projectId,
            SourceLocaleId = sourceLocaleId,
            TargetLocaleId = targetLocaleId,
            JobId = jobId
        };
    }

    private async Task<List<LocaleResponse>> GetProjectLocales(string projectId, string? branch)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/locales", Method.Get);
        if (!string.IsNullOrWhiteSpace(branch))
            request.AddQueryParameter("branch", branch);

        return await Client.Paginate<LocaleResponse>(request);
    }

    private async Task<CreateJobResponse> GetJob(string projectId, string jobId, string? branch)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/jobs/{jobId}", Method.Get);
        if (!string.IsNullOrWhiteSpace(branch))
            request.AddQueryParameter("branch", branch);

        return await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);
    }

    private async Task<ProjectResponse> GetProject(string projectId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}", Method.Get);
        return await Client.ExecuteWithErrorHandling<ProjectResponse>(request);
    }

    private async Task<List<KeyResponse>> ResolveKeys(
        string projectId,
        string? branch,
        IEnumerable<string>? keyIds,
        IEnumerable<string>? keyNames,
        CreateJobResponse? job)
    {
        var requestedIds = keyIds?.Where(id => !string.IsNullOrWhiteSpace(id)).ToHashSet(StringComparer.Ordinal) ?? [];
        var requestedNames = keyNames?.Where(name => !string.IsNullOrWhiteSpace(name)).ToHashSet(StringComparer.Ordinal) ?? [];
        var jobIds = job?.Keys?.Select(key => key.Id).Where(id => !string.IsNullOrWhiteSpace(id)).ToHashSet(StringComparer.Ordinal) ?? [];

        var request = new RestRequest($"/v2/projects/{projectId}/keys", Method.Get);
        if (!string.IsNullOrWhiteSpace(branch))
            request.AddQueryParameter("branch", branch);

        var keys = await Client.Paginate<KeyResponse>(request);

        if (requestedIds.Count > 0)
        {
            var availableIds = keys.Select(key => key.Id).Where(id => !string.IsNullOrWhiteSpace(id)).ToHashSet(StringComparer.Ordinal);
            var missingIds = requestedIds.Where(id => !availableIds.Contains(id)).ToList();
            if (missingIds.Count > 0)
                throw new PluginMisconfigurationException($"Key IDs not found: {string.Join(", ", missingIds)}.");
        }

        if (requestedNames.Count > 0)
        {
            var availableNames = keys.Select(key => key.Name).Where(name => !string.IsNullOrWhiteSpace(name)).ToHashSet(StringComparer.Ordinal);
            var missingNames = requestedNames.Where(name => !availableNames.Contains(name)).ToList();
            if (missingNames.Count > 0)
                throw new PluginMisconfigurationException($"Key names not found: {string.Join(", ", missingNames)}.");
        }

        var hasDirectFilter = requestedIds.Count > 0 || requestedNames.Count > 0;
        if (hasDirectFilter)
        {
            keys = keys
                .Where(key => requestedIds.Contains(key.Id) || requestedNames.Contains(key.Name))
                .ToList();
        }

        if (job is not null)
            keys = keys.Where(key => jobIds.Contains(key.Id)).ToList();

        return keys;
    }

    private async Task<List<TranslationResponse>> GetTranslationsByLocale(string projectId, string localeId, string? branch)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/locales/{localeId}/translations", Method.Get);
        if (!string.IsNullOrWhiteSpace(branch))
            request.AddQueryParameter("branch", branch);

        try
        {
            return await Client.Paginate<TranslationResponse>(request);
        }
        catch (PluginApplicationException ex) when (ex.Message.Contains("TRANSLATIONS_NOT_FOUND", StringComparison.Ordinal))
        {
            return [];
        }
    }

    private async Task<Dictionary<string, QualityScoreResponse>> GetTranslationQualityScores(
        string projectId,
        IEnumerable<string> translationIds)
    {
        var qualityScores = new Dictionary<string, QualityScoreResponse>(StringComparer.Ordinal);

        foreach (var batch in translationIds.Chunk(500))
        {
            var request = new RestRequest($"/v2/projects/{projectId}/quality_performance_score", Method.Post)
                .AddJsonBody(new { translation_ids = batch });
            var response = await Client.ExecuteWithErrorHandling<QualityScoreListResponse>(request);

            foreach (var qualityScore in response.Data?.Translations ?? [])
            {
                if (string.IsNullOrWhiteSpace(qualityScore.Id) || !qualityScore.Score.HasValue)
                    continue;

                qualityScores[qualityScore.Id] = qualityScore;
            }
        }

        return qualityScores;
    }

    private static LocaleResponse ResolveLocale(IEnumerable<LocaleResponse> locales, string value, string label)
    {
        var locale = locales.FirstOrDefault(locale =>
            string.Equals(locale.Id, value, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(locale.Code, value, StringComparison.OrdinalIgnoreCase));

        return locale ?? throw new PluginMisconfigurationException($"Could not resolve {label} locale from '{value}'.");
    }

    private static LocaleResponse ResolveSourceLocale(IEnumerable<LocaleResponse> locales, string? value, CreateJobResponse? job)
    {
        if (!string.IsNullOrWhiteSpace(value))
            return ResolveLocale(locales, value, "source");

        if (!string.IsNullOrWhiteSpace(job?.SourceLocale?.Id))
            return ResolveLocale(locales, job.SourceLocale!.Id!, "source");

        return locales.FirstOrDefault(locale => locale.IsDefault)
            ?? locales.FirstOrDefault(locale => locale.IsMain)
            ?? locales.FirstOrDefault()
            ?? throw new PluginMisconfigurationException("Project has no locales.");
    }

    private static Transformation CreateTransformation(
        string projectId,
        LocaleResponse sourceLocale,
        LocaleResponse targetLocale,
        string? branch,
        CreateJobResponse? job)
    {
        var transformation = new Transformation(sourceLocale.Code, targetLocale.Code)
        {
            BilingualMediaType = MediaTypes.Xliff2,
            OriginalName = $"phrase-strings-{projectId}",
            SourceSystemReference =
            {
                ContentId = projectId,
                ContentName = sourceLocale.Code,
                SystemName = "Phrase Strings",
                SystemRef = "https://phrase.com/platform/strings/"
            },
            TargetSystemReference =
            {
                ContentId = projectId,
                ContentName = targetLocale.Code,
                SystemName = "Phrase Strings",
                SystemRef = "https://phrase.com/platform/strings/"
            }
        };

        AddOrUpdateMetadata(transformation, ProjectIdMeta, projectId);
        AddOrUpdateMetadata(transformation, SourceLocaleIdMeta, sourceLocale.Id);
        AddOrUpdateMetadata(transformation, SourceLocaleCodeMeta, sourceLocale.Code);
        AddOrUpdateMetadata(transformation, TargetLocaleIdMeta, targetLocale.Id);
        AddOrUpdateMetadata(transformation, TargetLocaleCodeMeta, targetLocale.Code);
        AddOrUpdateMetadata(transformation, BranchMeta, branch);
        AddOrUpdateMetadata(transformation, JobIdMeta, job?.Id);

        return transformation;
    }

    private static Unit CreateUnit(
        PlaintextCoder coder,
        KeyResponse key,
        TranslationResponse? sourceTranslation,
        TranslationResponse? targetTranslation)
    {
        var unit = new Unit(coder)
        {
            Name = key.Name,
            Provenance =
            {
                Translation =
                {
                    Tool = "Phrase Strings",
                    Person = FirstNotEmpty(targetTranslation?.User?.Name, targetTranslation?.User?.Username)
                }
            }
        };

        if (key.MaxCharactersAllowed.GetValueOrDefault() > 0)
            unit.SizeRestrictions.MaximumSize = key.MaxCharactersAllowed;

        var state = GetSegmentState(targetTranslation);
        if (state is SegmentState.Final)
            unit.Provenance.Review.Person = FirstNotEmpty(targetTranslation?.User?.Name, targetTranslation?.User?.Username);

        if (!string.IsNullOrWhiteSpace(key.Description))
            unit.Notes.Add(new Note(key.Description!));

        var segment = new Segment(coder)
        {
            Order = 0,
            Source = coder.DeserializeSegment(sourceTranslation?.Content ?? string.Empty),
            Target = coder.DeserializeSegment(targetTranslation?.Content ?? string.Empty),
            State = state
        };
        SetSegmentId(segment, targetTranslation?.Id);

        unit.Segments.Add(segment);
        return unit;
    }

    private static SegmentState GetSegmentState(TranslationResponse? translation)
    {
        if (translation is null || string.IsNullOrEmpty(translation.Content))
            return SegmentState.Initial;

        if (string.Equals(translation.State, "reviewed", StringComparison.OrdinalIgnoreCase))
            return SegmentState.Final;

        if (translation.Unverified == false)
            return SegmentState.Reviewed;

        return SegmentState.Translated;
    }

    private static void SetSegmentId(Segment segment, string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return;

        var setter = typeof(Segment)
            .GetProperty(nameof(Segment.Id), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            ?.GetSetMethod(true);

        if (setter is null)
            throw new PluginApplicationException("Could not set XLIFF segment ID: Segment.Id setter was not found.");

        try
        {
            setter.Invoke(segment, new object?[] { id });
        }
        catch (Exception ex)
        {
            throw new PluginApplicationException($"Could not set XLIFF segment ID '{id}'.", ex);
        }
    }

    private async Task<TranslationResponse> CreateTranslation(
        string projectId,
        string targetLocaleId,
        string keyId,
        string content,
        string? branch,
        SegmentState state,
        HashSet<SegmentState> statesToApply)
    {
        var shouldApplyState = statesToApply.Contains(state);
        var body = new Dictionary<string, object?>
        {
            ["locale_id"] = targetLocaleId,
            ["key_id"] = keyId,
            ["content"] = content,
            ["branch"] = branch,
            ["reviewed"] = shouldApplyState && state == SegmentState.Final ? true : null,
            ["unverified"] = shouldApplyState && state is SegmentState.Reviewed or SegmentState.Final ? false : null
        };

        var request = new RestRequest($"/v2/projects/{projectId}/translations", Method.Post)
            .AddJsonBody(body.Where(pair => pair.Value is not null).ToDictionary(pair => pair.Key, pair => pair.Value));

        return await Client.ExecuteWithErrorHandling<TranslationResponse>(request);
    }

    private async Task UpdateTranslation(
        string projectId,
        string translationId,
        string content,
        string? branch,
        SegmentState state,
        HashSet<SegmentState> statesToApply)
    {
        var shouldApplyState = statesToApply.Contains(state);
        var body = new Dictionary<string, object?>
        {
            ["content"] = content,
            ["branch"] = branch,
            ["reviewed"] = shouldApplyState && state == SegmentState.Final ? true : null,
            ["unverified"] = shouldApplyState && state is SegmentState.Reviewed or SegmentState.Final ? false : null
        };

        var request = new RestRequest($"/v2/projects/{projectId}/translations/{translationId}", Method.Patch)
            .AddJsonBody(body.Where(pair => pair.Value is not null).ToDictionary(pair => pair.Key, pair => pair.Value));

        await Client.ExecuteWithErrorHandling(request);
    }

    private async Task AddTags(string projectId, string targetLocaleId, string? branch, IEnumerable<string> keyIds, string tags)
    {
        var body = new Dictionary<string, object?>
        {
            ["branch"] = branch,
            ["locale_id"] = targetLocaleId,
            ["q"] = $"ids:{string.Join(",", keyIds)}",
            ["tags"] = tags
        };

        var request = new RestRequest($"/v2/projects/{projectId}/keys/tag", Method.Patch)
            .AddJsonBody(body.Where(pair => pair.Value is not null).ToDictionary(pair => pair.Key, pair => pair.Value));

        await Client.ExecuteWithErrorHandling(request);
    }

    private static void AddOrUpdateMetadata(MetadataContainingNode node, string type, string? value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        var existing = node.MetaData.FirstOrDefault(meta =>
            meta.Type == type && meta.Category.Contains(MetadataCategory));

        if (existing is not null)
        {
            existing.Value = value;
            return;
        }

        node.MetaData.Add(new Metadata(type, value) { Category = [MetadataCategory] });
    }

    private static string? GetMetadata(MetadataContainingNode node, string type)
    {
        return node.MetaData.FirstOrDefault(meta =>
            meta.Type == type && meta.Category.Contains(MetadataCategory))?.Value;
    }

    private static string? FirstNotEmpty(params string?[] values)
    {
        return values.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
    }

    private static string CreateDownloadFileName(ProjectResponse project, LocaleResponse targetLocale, CreateJobResponse? job)
    {
        var projectName = SanitizeFileName(project.Name);
        var targetLocaleCode = SanitizeFileName(targetLocale.Code);
        var jobSuffix = job is null ? null : "job";
        var timestamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-'Z'");
        var parts = new[] { "phrase-strings", projectName, targetLocaleCode, jobSuffix, timestamp }
            .Where(part => !string.IsNullOrWhiteSpace(part));

        return $"{string.Join("-", parts)}.xliff";
    }

    private static string SanitizeFileName(string value)
    {
        var invalidChars = Path.GetInvalidFileNameChars().ToHashSet();
        var sanitizedChars = value
            .Trim()
            .Select(character => invalidChars.Contains(character) || char.IsWhiteSpace(character) ? '-' : character)
            .ToArray();

        var sanitized = new string(sanitizedChars).Trim('-');
        return string.IsNullOrWhiteSpace(sanitized) ? "project" : sanitized;
    }
}
