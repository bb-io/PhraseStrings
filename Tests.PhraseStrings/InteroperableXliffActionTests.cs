using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Api;
using Apps.PhraseStrings.Constants;
using Apps.PhraseStrings.Model.InteroperableXliff;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Translation;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Filters.Constants;
using Blackbird.Filters.Enums;
using Blackbird.Filters.Extensions;
using Blackbird.Filters.Transformations;
using Newtonsoft.Json;
using RestSharp;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class InteroperableXliffActionTests : TestBaseMultipleConnections
{
    private const string InitialStateFile = "phrase-keys-xliff-initial-state.json";
    private const string FinalStateFile = "phrase-keys-xliff-final-state.json";

    [TestMethod]
    public async Task DownloadAndUploadKeys_RoundTrip_IsSuccess()
    {
        var initialState = LoadState(InitialStateFile);
        var finalState = LoadState(FinalStateFile);
        var context = GetInvocationContext("Access token");
        await AssertApiHostReachable(context);

        var client = new PhraseStringsClient(context.AuthenticationCredentialsProviders);
        var projectActions = new ProjectActions(context, FileManager);
        var keyActions = new KeyActions(context);
        var xliffActions = new InteroperableXliffActions(context, FileManager);

        RoundTripProject? project = null;

        try
        {
            project = await CreateInitialProjectState(client, projectActions, keyActions, initialState);

            var download = await xliffActions.DownloadKeys(
                new ProjectRequest { ProjectId = project.Project.Id },
                new DownloadKeysRequest
                {
                    SourceLocale = project.SourceLocale.Id,
                    TargetLocale = project.TargetLocale.Id,
                    KeyIds = project.KeysByName.Values.Select(key => key.Id)
                });

            AssertDownloadMatchesInitialState(download, project, initialState);

            var editedXliff = CreateFinalStateXliff(download, project, finalState);
            var upload = await xliffActions.UploadKeys(new UploadKeysRequest
            {
                Content = editedXliff,
                Tags = finalState.UploadTags
            });

            Assert.AreEqual(project.KeysByName.Count, upload.KeysUploaded);
            Assert.AreEqual(project.Project.Id, upload.ProjectId);
            Assert.AreEqual(project.SourceLocale.Id, upload.SourceLocaleId);
            Assert.AreEqual(project.TargetLocale.Id, upload.TargetLocaleId);

            await AssertFinalProjectState(client, project, finalState);
        }
        finally
        {
            if (project is not null)
                await DeleteProject(client, project.Project.Id);
        }
    }

    private RoundTripState LoadState(string fileName)
    {
        using var stream = FileManager.DownloadAsync(new() { Name = fileName }).GetAwaiter().GetResult();
        using var reader = new StreamReader(stream);
        return JsonConvert.DeserializeObject<RoundTripState>(reader.ReadToEnd())
            ?? throw new InvalidOperationException($"Could not deserialize {fileName}.");
    }

    private async Task<RoundTripProject> CreateInitialProjectState(
        PhraseStringsClient client,
        ProjectActions projectActions,
        KeyActions keyActions,
        RoundTripState state)
    {
        var project = await projectActions.CreateProject(
            new CreateProjectRequest
            {
                Name = $"{state.ProjectNamePrefix} {Guid.NewGuid():N}",
                MainFormat = state.MainFormat,
                Workflow = state.Workflow
            },
            new FileRequest());

        var sourceLocale = await GetOrCreateSourceLocale(client, project.Id, state.SourceLocale);
        var targetLocale = await CreateLocale(client, project.Id, state.TargetLocale.Name, state.TargetLocale.Code, sourceLocale.Id);
        var keysByName = new Dictionary<string, KeyResponse>(StringComparer.Ordinal);
        var initialTargetTranslationIds = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var keyState in state.Keys)
        {
            var key = await CreateKey(keyActions, project.Id, keyState.Name, keyState.Description, keyState.MaxCharactersAllowed);
            keysByName[key.Name] = key;

            await CreateTranslation(client, project.Id, sourceLocale.Id, key.Id, keyState.SourceContent, null, null);

            if (!string.IsNullOrEmpty(keyState.TargetContent))
            {
                var targetTranslation = await CreateTranslation(
                    client,
                    project.Id,
                    targetLocale.Id,
                    key.Id,
                    keyState.TargetContent,
                    keyState.IsTargetVerified.HasValue ? !keyState.IsTargetVerified.Value : null,
                    null);

                if (keyState.IsTargetReviewed == true)
                    targetTranslation = await ReviewTranslation(client, project.Id, targetTranslation.Id);

                initialTargetTranslationIds[key.Name] = targetTranslation.Id;
            }
        }

        return new RoundTripProject(project, sourceLocale, targetLocale, keysByName, initialTargetTranslationIds);
    }

    private void AssertDownloadMatchesInitialState(
        DownloadKeysResponse download,
        RoundTripProject project,
        RoundTripState initialState)
    {
        Assert.AreEqual(initialState.Keys.Count, download.TotalKeysDownloaded);
        Assert.AreEqual(initialState.Keys.Count(key => key.DownloadState == "initial"), download.TotalKeysWithoutTargetTranslations);
        Assert.AreEqual(initialState.Keys.Count(key => key.DownloadState == "translated"), download.TotalKeysWithUnverifiedTargetTranslations);
        Assert.AreEqual(initialState.Keys.Count(key => key.DownloadState is "reviewed" or "final"), download.TotalKeysWithVerifiedTargetTranslations);
        Assert.AreEqual(initialState.Keys.Count(key => key.IsTargetReviewed == true), download.TotalKeysWithReviewedTargetTranslations);
        Assert.AreEqual(project.SourceLocale.Id, download.SourceLocaleId);
        Assert.AreEqual(project.TargetLocale.Id, download.TargetLocaleId);
        StringAssert.StartsWith(download.Content.Name, "phrase-strings-Blackbird-XLIFF-round-trip-");
        StringAssert.Contains(download.Content.Name, $"-{project.TargetLocale.Code}-");
        StringAssert.EndsWith(download.Content.Name, ".xliff");
        Assert.IsFalse(download.Content.Name.Contains(project.Project.Id), download.Content.Name);
        Assert.IsFalse(download.Content.Name.Contains("-job-"), download.Content.Name);
        Assert.IsTrue(
            System.Text.RegularExpressions.Regex.IsMatch(download.Content.Name, @"\d{4}-\d{2}-\d{2}-\d{2}-\d{2}-\d{2}-Z\.xliff$"),
            $"Unexpected download file name: {download.Content.Name}");

        var xliff = FileManager.ReadOutputAsString(download.Content);
        var transformation = Transformation.Load(xliff.ToStream(), download.Content.Name, MediaTypes.Xliff2).Value!;
        var unitsByKeyId = transformation.GetUnits().ToDictionary(unit => unit.Id!);

        foreach (var keyState in initialState.Keys)
        {
            var key = project.KeysByName[keyState.Name];
            var unit = unitsByKeyId[key.Id];
            var segment = unit.Segments.Single();
            var unitPhraseMetadata = unit.MetaData
                .Where(metadata => metadata.Category.Contains("phrase-strings"))
                .ToArray();

            Assert.AreEqual(keyState.Name, unit.Name);
            Assert.AreEqual(0, unitPhraseMetadata.Length);
            Assert.AreEqual(keyState.Description, unit.Notes.Single().Text);
            Assert.AreEqual(keyState.MaxCharactersAllowed, unit.SizeRestrictions.MaximumSize);
            Assert.AreEqual(keyState.SourceContent, segment.GetSource());
            Assert.AreEqual(keyState.TargetContent ?? string.Empty, segment.GetTarget());
            Assert.AreEqual(keyState.DownloadState.ToSegmentState(), segment.State);

            if (project.InitialTargetTranslationIds.TryGetValue(keyState.Name, out var translationId))
                Assert.AreEqual(translationId, segment.Id);
            else
                Assert.IsTrue(string.IsNullOrWhiteSpace(segment.Id));
        }
    }

    private FileReference CreateFinalStateXliff(
        DownloadKeysResponse download,
        RoundTripProject project,
        RoundTripState finalState)
    {
        var xliff = FileManager.ReadOutputAsString(download.Content);
        var transformation = Transformation.Load(xliff.ToStream(), download.Content.Name, MediaTypes.Xliff2).Value!;
        var unitsByKeyId = transformation.GetUnits().ToDictionary(unit => unit.Id!);

        foreach (var keyState in finalState.Keys)
        {
            var key = project.KeysByName[keyState.Name];
            var segment = unitsByKeyId[key.Id].Segments.Single();
            segment.SetTarget(keyState.TargetContent ?? string.Empty);
            segment.State = keyState.UploadState.ToSegmentState();
        }

        var modifiedContent = transformation.Serialize(unit => unit.Id);
        return FileManager.CreateFileReferenceFromString(
            modifiedContent,
            MediaTypes.Xliff2,
            "roundtrip-upload-content.xliff");
    }

    private async Task AssertFinalProjectState(
        PhraseStringsClient client,
        RoundTripProject project,
        RoundTripState finalState)
    {
        var translations = await GetTranslations(client, project.Project.Id, project.TargetLocale.Id);
        var translationsByKeyId = translations.ToDictionary(translation => translation.Key!.Id);

        foreach (var keyState in finalState.Keys)
        {
            var key = project.KeysByName[keyState.Name];
            var translation = translationsByKeyId[key.Id];

            Assert.AreEqual(keyState.TargetContent, translation.Content);

            if (keyState.IsTargetVerified.HasValue)
                Assert.AreEqual(!keyState.IsTargetVerified.Value, translation.Unverified);

            if (keyState.IsTargetReviewed.HasValue)
                Assert.AreEqual(keyState.IsTargetReviewed.Value, IsReviewed(translation));

            if (keyState.MustKeepExistingTranslationId)
                Assert.AreEqual(project.InitialTargetTranslationIds[keyState.Name], translation.Id);
            else if (!project.InitialTargetTranslationIds.ContainsKey(keyState.Name))
                Assert.IsFalse(string.IsNullOrWhiteSpace(translation.Id));
        }

        if (!string.IsNullOrWhiteSpace(finalState.UploadTags))
        {
            foreach (var key in project.KeysByName.Values)
            {
                var taggedKey = await GetKey(client, project.Project.Id, key.Id);
                CollectionAssert.Contains(taggedKey.Tags, finalState.UploadTags);
            }
        }
    }

    private static async Task<LocaleResponse> GetOrCreateSourceLocale(
        PhraseStringsClient client,
        string projectId,
        LocaleState sourceLocaleState)
    {
        var locales = await GetLocales(client, projectId);
        var sourceLocale = locales.FirstOrDefault(locale =>
            locale.IsDefault ||
            string.Equals(locale.Code, sourceLocaleState.Code, StringComparison.OrdinalIgnoreCase));

        return sourceLocale ?? await CreateLocale(client, projectId, sourceLocaleState.Name, sourceLocaleState.Code, null, true);
    }

    private static async Task<List<LocaleResponse>> GetLocales(PhraseStringsClient client, string projectId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/locales", Method.Get);
        return await client.Paginate<LocaleResponse>(request);
    }

    private static async Task<LocaleResponse> CreateLocale(
        PhraseStringsClient client,
        string projectId,
        string name,
        string code,
        string? sourceLocaleId,
        bool isDefault = false)
    {
        var body = new Dictionary<string, object?>
        {
            ["name"] = name,
            ["code"] = code,
            ["default"] = isDefault,
            ["main"] = isDefault,
            ["source_locale_id"] = sourceLocaleId
        };

        var request = new RestRequest($"/v2/projects/{projectId}/locales", Method.Post)
            .AddJsonBody(body.Where(pair => pair.Value is not null).ToDictionary(pair => pair.Key, pair => pair.Value));

        return await client.ExecuteWithErrorHandling<LocaleResponse>(request);
    }

    private static async Task<KeyResponse> CreateKey(
        KeyActions keyActions,
        string projectId,
        string name,
        string description,
        int limit)
    {
        return await keyActions.CreateKey(
            new ProjectRequest { ProjectId = projectId },
            new CreateKeyRequest
            {
                Name = name,
                Description = description,
                MaxCharactersAllowed = limit
            });
    }

    private static async Task<TranslationResponse> CreateTranslation(
        PhraseStringsClient client,
        string projectId,
        string localeId,
        string keyId,
        string content,
        bool? unverified,
        bool? reviewed)
    {
        var body = new Dictionary<string, object?>
        {
            ["locale_id"] = localeId,
            ["key_id"] = keyId,
            ["content"] = content,
            ["unverified"] = unverified,
            ["reviewed"] = reviewed
        };

        var request = new RestRequest($"/v2/projects/{projectId}/translations", Method.Post)
            .AddJsonBody(body.Where(pair => pair.Value is not null).ToDictionary(pair => pair.Key, pair => pair.Value));

        return await client.ExecuteWithErrorHandling<TranslationResponse>(request);
    }

    private static async Task<List<TranslationResponse>> GetTranslations(
        PhraseStringsClient client,
        string projectId,
        string localeId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/locales/{localeId}/translations", Method.Get);
        return await client.Paginate<TranslationResponse>(request);
    }

    private static async Task<KeyResponse> GetKey(PhraseStringsClient client, string projectId, string keyId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/keys/{keyId}", Method.Get);
        return await client.ExecuteWithErrorHandling<KeyResponse>(request);
    }

    private static bool IsReviewed(TranslationResponse translation)
    {
        return string.Equals(translation.State, "reviewed", StringComparison.OrdinalIgnoreCase);
    }

    private static async Task<TranslationResponse> ReviewTranslation(
        PhraseStringsClient client,
        string projectId,
        string translationId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}/translations/{translationId}/review", Method.Patch)
            .AddJsonBody(new Dictionary<string, object>());

        return await client.ExecuteWithErrorHandling<TranslationResponse>(request);
    }

    private static async Task DeleteProject(PhraseStringsClient client, string projectId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}", Method.Delete);
        await client.ExecuteWithErrorHandling(request);
    }

    private static async Task AssertApiHostReachable(InvocationContext context)
    {
        var url = context.AuthenticationCredentialsProviders.First(provider => provider.KeyName == CredsNames.Url).Value;
        var host = new Uri(url).Host;

        try
        {
            await System.Net.Dns.GetHostAddressesAsync(host);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Phrase Strings API host is not reachable from this environment: {ex.Message}");
        }
    }

    private record RoundTripProject(
        ProjectResponse Project,
        LocaleResponse SourceLocale,
        LocaleResponse TargetLocale,
        Dictionary<string, KeyResponse> KeysByName,
        Dictionary<string, string> InitialTargetTranslationIds);

    private class RoundTripState
    {
        public string ProjectNamePrefix { get; set; } = "Blackbird XLIFF round trip";
        public string MainFormat { get; set; } = "json";
        public string? Workflow { get; set; }
        public LocaleState SourceLocale { get; set; } = new();
        public LocaleState TargetLocale { get; set; } = new();
        public string? UploadTags { get; set; }
        public List<KeyState> Keys { get; set; } = [];
    }

    private class LocaleState
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    private class KeyState
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MaxCharactersAllowed { get; set; }
        public string SourceContent { get; set; } = string.Empty;
        public string? TargetContent { get; set; }
        public bool? IsTargetVerified { get; set; }
        public bool? IsTargetReviewed { get; set; }
        public string DownloadState { get; set; } = "initial";
        public string UploadState { get; set; } = "translated";
        public bool MustKeepExistingTranslationId { get; set; }
    }
}
