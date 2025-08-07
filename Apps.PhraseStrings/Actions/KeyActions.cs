using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList("Keys")]
    public class KeyActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {
        [Action("Search keys", Description = "Searches keys")]
        public async Task<ListKeysResponse> SearchKeys([ActionParameter] ProjectRequest project,
            [ActionParameter] SearchKeysRequest input)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys", Method.Get);

            if (!string.IsNullOrEmpty(input.Branch))
            {
                request.AddQueryParameter("branch", input.Branch);
            }

            if (!string.IsNullOrEmpty(input.Sort))
            {
                request.AddQueryParameter("sort", input.Sort);
            }

            if (!string.IsNullOrEmpty(input.Order))
            {
                request.AddQueryParameter("order", input.Order);
            }

            if (!string.IsNullOrEmpty(input.LocaleId))
            {
                request.AddQueryParameter("locale_id", input.LocaleId);
            }

            if (input.Tags != null && input.Tags.Any())
            {
                var joinedTags = string.Join(",", input.Tags);
                request.AddQueryParameter("q", $"tags:{joinedTags}");
            }

            var keys = await Client.Paginate<KeyResponse>(request);

            return new ListKeysResponse
            {
                Keys = keys,
                KeyIds = keys?.Select(k => k.Id).ToList()
            };
        }


        [Action("Get key by name", Description = "Gets detailed key information from a key name")]
        public async Task<KeyResponse> GetKeyByName(
            [ActionParameter] ProjectRequest project,
            [ActionParameter] BranchRequest branch,
            [ActionParameter] KeyNameRequest key)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys", Method.Get);

            if (!string.IsNullOrEmpty(branch.Branch))
                request.AddQueryParameter("branch", branch.Branch);

            if (!string.IsNullOrEmpty(key.KeyName))
                request.AddQueryParameter("q", $"name:{key.KeyName}");
            else
                throw new PluginMisconfigurationException("Key name cannot be empty");

            var searchResponse = await Client.ExecuteWithErrorHandling<List<KeyResponse>>(request);
            var keyFound = searchResponse?.FirstOrDefault();

            return keyFound ?? new KeyResponse();
        }

        [Action("Create a key", Description = "Creates a key")]
        public async Task<KeyResponse> CreateKey([ActionParameter] ProjectRequest project,
            [ActionParameter] CreateKeyRequest input)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys", Method.Post);

            var body = new Dictionary<string, object>
            {
                ["name"] = input.Name
            };

            if (!string.IsNullOrEmpty(input.Branch))
                body["branch"] = input.Branch;

            if (!string.IsNullOrEmpty(input.Description))
                body["description"] = input.Description;

            if (input.Plural.HasValue)
                body["plural"] = input.Plural.Value;

            if (!string.IsNullOrEmpty(input.NamePlural))
                body["name_plural"] = input.NamePlural;

            if (!string.IsNullOrEmpty(input.DataType))
                body["data_type"] = input.DataType;

            if (!string.IsNullOrEmpty(input.Tags))
                body["tags"] = input.Tags;

            if (input.MaxCharactersAllowed.HasValue)
                body["max_characters_allowed"] = input.MaxCharactersAllowed.Value;

            if (input.Unformatted.HasValue)
                body["unformatted"] = input.Unformatted.Value;

            if (!string.IsNullOrEmpty(input.DefaultTranslationContent))
                body["default_translation_content"] = input.DefaultTranslationContent;

            if (input.Autotranslate.HasValue)
                body["autotranslate"] = input.Autotranslate.Value;

            if (input.XmlSpacePreserve.HasValue)
                body["xml_space_preserve"] = input.XmlSpacePreserve.Value;

            if (!string.IsNullOrEmpty(input.OriginalFile))
                body["original_file"] = input.OriginalFile;

            if (!string.IsNullOrEmpty(input.LocalizedFormatString))
                body["localized_format_string"] = input.LocalizedFormatString;

            if (!string.IsNullOrEmpty(input.LocalizedFormatKey))
                body["localized_format_key"] = input.LocalizedFormatKey;

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            var key = await Client.ExecuteWithErrorHandling<KeyResponse>(request);
            return key;

        }

        [Action("Update a key", Description = "Updates a key")]
        public async Task<KeyResponse> UpdateKey([ActionParameter] ProjectRequest project,
            [ActionParameter] CreateKeyRequest input, [ActionParameter] KeyRequest keyinput)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys/{keyinput.KeyId}", Method.Patch);

            var body = new Dictionary<string, object>{};

            if (!string.IsNullOrEmpty(input.Branch))
                body["branch"] = input.Branch;

            if (!string.IsNullOrEmpty(input.Name))
                body["name"] = input.Name;

            if (!string.IsNullOrEmpty(input.Description))
                body["description"] = input.Description;

            if (input.Plural.HasValue)
                body["plural"] = input.Plural.Value;

            if (!string.IsNullOrEmpty(input.NamePlural))
                body["name_plural"] = input.NamePlural;

            if (!string.IsNullOrEmpty(input.DataType))
                body["data_type"] = input.DataType;

            if (!string.IsNullOrEmpty(input.Tags))
                body["tags"] = input.Tags;

            if (input.MaxCharactersAllowed.HasValue)
                body["max_characters_allowed"] = input.MaxCharactersAllowed.Value;

            if (input.Unformatted.HasValue)
                body["unformatted"] = input.Unformatted.Value;

            if (!string.IsNullOrEmpty(input.DefaultTranslationContent))
                body["default_translation_content"] = input.DefaultTranslationContent;

            if (input.Autotranslate.HasValue)
                body["autotranslate"] = input.Autotranslate.Value;

            if (input.XmlSpacePreserve.HasValue)
                body["xml_space_preserve"] = input.XmlSpacePreserve.Value;

            if (!string.IsNullOrEmpty(input.OriginalFile))
                body["original_file"] = input.OriginalFile;

            if (!string.IsNullOrEmpty(input.LocalizedFormatString))
                body["localized_format_string"] = input.LocalizedFormatString;

            if (!string.IsNullOrEmpty(input.LocalizedFormatKey))
                body["localized_format_key"] = input.LocalizedFormatKey;

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            var key = await Client.ExecuteWithErrorHandling<KeyResponse>(request);
            return key;

        }


        [Action("Add tags to keys", Description ="Adds tags to specified keys")]
        public async Task<ResordAffectedResponse> AddtagsToKeys([ActionParameter] ProjectRequest project,
            [ActionParameter] AddTagsToKeysRequest tags)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys/tag", Method.Patch);
            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(tags.Branch))
                body["branch"] = tags.Branch;

            if (!string.IsNullOrEmpty(tags.LocaleId))
                body["locale_id"] = tags.LocaleId;

            if (!string.IsNullOrEmpty(tags.Tags))
                body["tags"] = tags.Tags;

            if (!string.IsNullOrEmpty(tags.Query))
                body["q"] = tags.Query;

            if (body.Count > 0)
                request.AddJsonBody(body);

            return await Client.ExecuteWithErrorHandling<ResordAffectedResponse>(request);
        }

        [Action("Remove tags from keys", Description = "Removes tags from keys")]
        public async Task<ResordAffectedResponse> RemovetagsToKeys([ActionParameter] ProjectRequest project,
            [ActionParameter] AddTagsToKeysRequest tags)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys/untag", Method.Patch);
            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(tags.Branch))
                body["branch"] = tags.Branch;

            if (!string.IsNullOrEmpty(tags.LocaleId))
                body["locale_id"] = tags.LocaleId;

            if (!string.IsNullOrEmpty(tags.Tags))
                body["tags"] = tags.Tags;

            if (!string.IsNullOrEmpty(tags.Query))
                body["q"] = tags.Query;

            if (body.Count > 0)
                request.AddJsonBody(body);

            return await Client.ExecuteWithErrorHandling<ResordAffectedResponse>(request);
        }

    }
}
