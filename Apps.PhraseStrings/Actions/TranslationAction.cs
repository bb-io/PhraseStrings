using Apps.PhraseStrings.Model;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Order;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Transactions;

namespace Apps.PhraseStrings.Actions
{
    [ActionList]
    public class TranslationAction(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : PhraseStringsInvocable(invocationContext)
    {
        [Action("Create translation", Description = "Creates translation")]
        public async Task<TranslationResponse> CreateTranslation([ActionParameter] ProjectRequest project,
            [ActionParameter] CreateTranslationRequest options)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/translations", Method.Post);

            var jsonBody = JsonConvert.SerializeObject(options,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               });

            return await Client.ExecuteWithErrorHandling<TranslationResponse>(request);
        }

        [Action("Update translation", Description = "Updates translation")]
        public async Task<TranslationResponse> UpdateTranslation([ActionParameter] ProjectRequest project,
            [ActionParameter] UpdateTranslationRequest options,
            [ActionParameter] TranslationRequest translation)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/translations/{translation.TranslationId}", Method.Post);

            var jsonBody = JsonConvert.SerializeObject(options,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               });

            return await Client.ExecuteWithErrorHandling<TranslationResponse>(request);
        }


        [Action("Get translations for key", Description = "Gets translations for key")]
        public async Task<ListTranslationsResponse> GetTranslationForKey([ActionParameter] ProjectRequest project,
            [ActionParameter] KeyRequest key, [ActionParameter] GetTranslationsForKeyRequest options)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys/{key.KeyId}/translations", Method.Get);

            var jsonBody = JsonConvert.SerializeObject(options,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               });

            var response = await Client.Paginate<TranslationResponse>(request);

            return new ListTranslationsResponse { Translations = response };
        }

        [Action("Get translations for locale", Description = "Gets translations for locale")]
        public async Task<ListTranslationsResponse> GetTranslationForLocale([ActionParameter] ProjectRequest project,
            [ActionParameter] LocaleRequest locale, [ActionParameter] GetTranslationsForLocaleRequest option)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/locales/{locale.LocaleId}/translations", Method.Post);

            var jsonBody = JsonConvert.SerializeObject(option,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               });

            var response = await Client.Paginate<TranslationResponse>(request);

            return new ListTranslationsResponse { Translations = response };
        }


        [Action("Download locale", Description = "Downloads locale")]
        public async Task<FileResponse> DownloadLocale([ActionParameter] ProjectRequest project,
            [ActionParameter] LocaleRequest locale, [ActionParameter] DownloadLocaleRequest options)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/locales/{locale.LocaleId}/download", Method.Get);

            if (!string.IsNullOrEmpty(options.Branch))
                request.AddQueryParameter("branch", options.Branch);

            if (!string.IsNullOrEmpty(options.FileFormat))
                request.AddQueryParameter("file_format", options.FileFormat);

            if (!string.IsNullOrEmpty(options.Tags))
                request.AddQueryParameter("tags", options.Tags);

            if (options.IncludeEmptyTranslations.HasValue)
                request.AddQueryParameter("include_empty_translations", options.IncludeEmptyTranslations.Value.ToString().ToLowerInvariant());

            if (options.ExcludeEmptyZeroForms.HasValue)
                request.AddQueryParameter("exclude_empty_zero_forms", options.ExcludeEmptyZeroForms.Value.ToString().ToLowerInvariant());

            if (options.IncludeTranslatedKeys.HasValue)
                request.AddQueryParameter("include_translated_keys", options.IncludeTranslatedKeys.Value.ToString().ToLowerInvariant());

            if (options.KeepNotranslateTags.HasValue)
                request.AddQueryParameter("keep_notranslate_tags", options.KeepNotranslateTags.Value.ToString().ToLowerInvariant());

            if (!string.IsNullOrEmpty(options.Encoding))
                request.AddQueryParameter("encoding", options.Encoding);

            if (options.IncludeUnverifiedTranslations.HasValue)
                request.AddQueryParameter("include_unverified_translations", options.IncludeUnverifiedTranslations.Value.ToString().ToLowerInvariant());

            if (options.UseLastReviewedVersion.HasValue)
                request.AddQueryParameter("use_last_reviewed_version", options.UseLastReviewedVersion.Value.ToString().ToLowerInvariant());

            if (!string.IsNullOrEmpty(options.FallbackLocaleId))
                request.AddQueryParameter("fallback_locale_id", options.FallbackLocaleId);

            if (!string.IsNullOrEmpty(options.SourceLocaleId))
                request.AddQueryParameter("source_locale_id", options.SourceLocaleId);

            if (!string.IsNullOrEmpty(options.TranslationKeyPrefix))
                request.AddQueryParameter("translation_key_prefix", options.TranslationKeyPrefix);

            if (options.FilterByPrefix.HasValue)
                request.AddQueryParameter("filter_by_prefix", options.FilterByPrefix.Value.ToString().ToLowerInvariant());

            if (options.LocaleIds != null && options.LocaleIds.Any())
            {
                var csv = string.Join(",", options.LocaleIds);
                request.AddQueryParameter("locale_ids", csv);
            }

            var responseDownload = await Client.ExecuteWithErrorHandling(request);

            var fileData = responseDownload.RawBytes;

            string fileName = null;
            var contentDisposition = responseDownload.ContentHeaders
                .FirstOrDefault(h => h.Name.Equals("Content-Disposition", StringComparison.OrdinalIgnoreCase))?
                .Value?.ToString();
            if (!string.IsNullOrEmpty(contentDisposition) && contentDisposition.Contains("filename"))
            {
                var parts = contentDisposition.Split(';')
                    .Select(p => p.Trim())
                    .FirstOrDefault(p => p.StartsWith("filename=", StringComparison.OrdinalIgnoreCase));
                if (parts != null)
                {
                    fileName = parts.Substring("filename=".Length).Trim('"');
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                var ext = options.FileFormat ?? "txt";
                fileName = $"{locale.LocaleId}.{ext}";
            }

            using var stream = new MemoryStream(fileData);
            var fileRef = await fileManagementClient.UploadAsync(stream, responseDownload.ContentType, fileName);

            return new FileResponse
            {
                File = fileRef
            };
        }

        [Action("Upload  file", Description = "Uploads  file")]
        public async Task<ImportResponse> UploadFile([ActionParameter] ProjectRequest project,
            [ActionParameter] UploadFileRequest input)
        {
            if (input.File == null)
                throw new ArgumentNullException(nameof(input.File), "File is required");

            var request = new RestRequest($"/v2/projects/{project.ProjectId}/uploads", Method.Post)
            {
                AlwaysMultipartFormData = true
            };

            var downloaded = await fileManagementClient.DownloadAsync(input.File);
            var bytes = await downloaded.GetByteData();

            request.AddFile("file", bytes, input.File.Name, input.File.ContentType);


            if (!string.IsNullOrEmpty(input.Branch))
                request.AddParameter("branch", input.Branch);
            if (!string.IsNullOrEmpty(input.FileFormat))
                request.AddParameter("file_format", input.FileFormat);
            if (!string.IsNullOrEmpty(input.LocaleId))
                request.AddParameter("locale_id", input.LocaleId);
            if (!string.IsNullOrEmpty(input.Tags))
                request.AddParameter("tags", input.Tags);

            if (input.UpdateTranslations.HasValue)
                request.AddParameter("update_translations", input.UpdateTranslations.Value);
            if (input.UpdateTranslationKeys.HasValue)
                request.AddParameter("update_translation_keys", input.UpdateTranslationKeys.Value);
            if (input.UpdateTranslationsOnSourceMatch.HasValue)
                request.AddParameter("update_translations_on_source_match", input.UpdateTranslationsOnSourceMatch.Value);
            if (input.UpdateDescriptions.HasValue)
                request.AddParameter("update_descriptions", input.UpdateDescriptions.Value);
            if (input.SkipUploadTags.HasValue)
                request.AddParameter("skip_upload_tags", input.SkipUploadTags.Value);
            if (input.SkipUnverification.HasValue)
                request.AddParameter("skip_unverification", input.SkipUnverification.Value);

            if (!string.IsNullOrEmpty(input.FileEncoding))
                request.AddParameter("file_encoding", input.FileEncoding);

            if (input.Autotranslate.HasValue)
                request.AddParameter("autotranslate", input.Autotranslate.Value);
            if (input.VerifyMentionedTranslations.HasValue)
                request.AddParameter("verify_mentioned_translations", input.VerifyMentionedTranslations.Value);
            if (input.MarkReviewed.HasValue)
                request.AddParameter("mark_reviewed", input.MarkReviewed.Value);
            if (input.TagOnlyAffectedKeys.HasValue)
                request.AddParameter("tag_only_affected_keys", input.TagOnlyAffectedKeys.Value);

            if (!string.IsNullOrEmpty(input.TranslationKeyPrefix))
                request.AddParameter("translation_key_prefix", input.TranslationKeyPrefix);

            var importResult = await Client.ExecuteWithErrorHandling<ImportResponse>(request);
            return importResult;
        }
    }
}
