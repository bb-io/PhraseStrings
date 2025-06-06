using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Order;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
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

            return new ListTranslationsResponse { Translations=response };
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
    }
}
