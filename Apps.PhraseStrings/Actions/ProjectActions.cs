using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext,IFileManagementClient fileManagementClient) : PhraseStringsInvocable(invocationContext)
{
    [Action("Search projects", Description = "Returnes list of projects")]
    public async Task<ListProjectsResponse> SearchProjects([ActionParameter] SearchProjectsRequest input)
    {
        var request = new RestRequest("/v2/projects", Method.Get);
        var projects = await Client.Paginate<ProjectResponse>(request);
        return new ListProjectsResponse { Projects = projects };
    }

    [Action("Get project", Description = "Gets a project")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectRequest input)
    {
        var request = new RestRequest($"/v2/projects/{input.ProjectId}", Method.Get);
        var project = await Client.ExecuteWithErrorHandling<ProjectResponse>(request);
        return project;
    }


    [Action("Delete project", Description = "Deletes a project")]
    public async Task DeleteProject([ActionParameter] ProjectRequest input)
    {
        var request = new RestRequest($"/v2/projects/{input.ProjectId}", Method.Delete);
        await Client.ExecuteWithErrorHandling(request);
    }


    [Action("Create project", Description = "Create a new project")]
    public async Task<ProjectResponse> CreateProject([ActionParameter] CreateProjectRequest input,
        [ActionParameter] FileRequest file)
    {
        if (string.IsNullOrEmpty(input.Name))
            throw new ArgumentException("Project name is required.");

        var request = new RestRequest("/v2/projects", Method.Post);

        byte[] projectImageBytes = null;
        if (file?.File != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                var fileStream = await fileManagementClient.DownloadAsync(file.File);
                projectImageBytes = await fileStream.GetByteData();
            }
        }

        var body = new Dictionary<string, object>
        {
            ["name"] = input.Name
        };

        if (!string.IsNullOrEmpty(input.MainFormat))
            body["main_format"] = input.MainFormat;

        if (!string.IsNullOrEmpty(input.Media))
            body["media"] = input.Media;

        if (input.SharesTranslationMemory.HasValue)
            body["shares_translation_memory"] = input.SharesTranslationMemory.Value;

        if (projectImageBytes != null && projectImageBytes.Length > 0)
        {
            string base64 = Convert.ToBase64String(projectImageBytes);
            body["project_image"] = base64;
        }

        if (input.RemoveProjectImage.HasValue)
            body["remove_project_image"] = input.RemoveProjectImage.Value;

        if (!string.IsNullOrEmpty(input.AccountId))
            body["account_id"] = input.AccountId;

        if (!string.IsNullOrEmpty(input.PointOfContact))
            body["point_of_contact"] = input.PointOfContact;

        if (!string.IsNullOrEmpty(input.SourceProjectId))
            body["source_project_id"] = input.SourceProjectId;

        if (!string.IsNullOrEmpty(input.Workflow))
            body["workflow"] = input.Workflow;

        if (input.MachineTranslationEnabled.HasValue)
            body["machine_translation_enabled"] = input.MachineTranslationEnabled.Value;

        if (input.EnableBranching.HasValue)
            body["enable_branching"] = input.EnableBranching.Value;

        if (input.ProtectMasterBranch.HasValue)
            body["protect_master_branch"] = input.ProtectMasterBranch.Value;

        if (input.EnableAllDataTypeTranslationKeysForTranslators.HasValue)
            body["enable_all_data_type_translation_keys_for_translators"] = input.EnableAllDataTypeTranslationKeysForTranslators.Value;

        if (input.EnableIcuMessageFormat.HasValue)
            body["enable_icu_message_format"] = input.EnableIcuMessageFormat.Value;

        if (input.ZeroPluralFormEnabled.HasValue)
            body["zero_plural_form_enabled"] = input.ZeroPluralFormEnabled.Value;

        if (input.AutotranslateEnabled.HasValue)
            body["autotranslate_enabled"] = input.AutotranslateEnabled.Value;

        if (input.AutotranslateCheckNewTranslationKeys.HasValue)
            body["autotranslate_check_new_translation_keys"] = input.AutotranslateCheckNewTranslationKeys.Value;

        if (input.AutotranslateCheckNewUploads.HasValue)
            body["autotranslate_check_new_uploads"] = input.AutotranslateCheckNewUploads.Value;

        if (input.AutotranslateCheckNewLocales.HasValue)
            body["autotranslate_check_new_locales"] = input.AutotranslateCheckNewLocales.Value;

        if (input.AutotranslateMarkAsUnverified.HasValue)
            body["autotranslate_mark_as_unverified"] = input.AutotranslateMarkAsUnverified.Value;

        if (input.AutotranslateUseMachineTranslation.HasValue)
            body["autotranslate_use_machine_translation"] = input.AutotranslateUseMachineTranslation.Value;

        if (input.AutotranslateUseTranslationMemory.HasValue)
            body["autotranslate_use_translation_memory"] = input.AutotranslateUseTranslationMemory.Value;


        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(body);

        var response = await Client.ExecuteWithErrorHandling<ProjectResponse>(request);
        return response;
    }


    [Action("Update project", Description = "Updates a project")]
    public async Task<ProjectResponse> UpdateProject([ActionParameter] CreateProjectRequest input,
        [ActionParameter] FileRequest file, [ActionParameter] ProjectRequest project)
    {
        if (string.IsNullOrEmpty(input.Name))
            throw new ArgumentException("Project name is required.");

        var request = new RestRequest($"/v2/projects/{project.ProjectId}", Method.Patch);

        byte[] projectImageBytes = null;
        if (file?.File != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                var fileStream = await fileManagementClient.DownloadAsync(file.File);
                projectImageBytes = await fileStream.GetByteData();
            }
        }

        var body = new Dictionary<string, object>
        {
            ["name"] = input.Name
        };

        if (!string.IsNullOrEmpty(input.MainFormat))
            body["main_format"] = input.MainFormat;

        if (!string.IsNullOrEmpty(input.Media))
            body["media"] = input.Media;

        if (input.SharesTranslationMemory.HasValue)
            body["shares_translation_memory"] = input.SharesTranslationMemory.Value;

        if (projectImageBytes != null && projectImageBytes.Length > 0)
        {
            string base64 = Convert.ToBase64String(projectImageBytes);
            body["project_image"] = base64;
        }

        if (input.RemoveProjectImage.HasValue)
            body["remove_project_image"] = input.RemoveProjectImage.Value;

        if (!string.IsNullOrEmpty(input.AccountId))
            body["account_id"] = input.AccountId;

        if (!string.IsNullOrEmpty(input.PointOfContact))
            body["point_of_contact"] = input.PointOfContact;

        if (!string.IsNullOrEmpty(input.SourceProjectId))
            body["source_project_id"] = input.SourceProjectId;

        if (!string.IsNullOrEmpty(input.Workflow))
            body["workflow"] = input.Workflow;

        if (input.MachineTranslationEnabled.HasValue)
            body["machine_translation_enabled"] = input.MachineTranslationEnabled.Value;

        if (input.EnableBranching.HasValue)
            body["enable_branching"] = input.EnableBranching.Value;

        if (input.ProtectMasterBranch.HasValue)
            body["protect_master_branch"] = input.ProtectMasterBranch.Value;

        if (input.EnableAllDataTypeTranslationKeysForTranslators.HasValue)
            body["enable_all_data_type_translation_keys_for_translators"] = input.EnableAllDataTypeTranslationKeysForTranslators.Value;

        if (input.EnableIcuMessageFormat.HasValue)
            body["enable_icu_message_format"] = input.EnableIcuMessageFormat.Value;

        if (input.ZeroPluralFormEnabled.HasValue)
            body["zero_plural_form_enabled"] = input.ZeroPluralFormEnabled.Value;

        if (input.AutotranslateEnabled.HasValue)
            body["autotranslate_enabled"] = input.AutotranslateEnabled.Value;

        if (input.AutotranslateCheckNewTranslationKeys.HasValue)
            body["autotranslate_check_new_translation_keys"] = input.AutotranslateCheckNewTranslationKeys.Value;

        if (input.AutotranslateCheckNewUploads.HasValue)
            body["autotranslate_check_new_uploads"] = input.AutotranslateCheckNewUploads.Value;

        if (input.AutotranslateCheckNewLocales.HasValue)
            body["autotranslate_check_new_locales"] = input.AutotranslateCheckNewLocales.Value;

        if (input.AutotranslateMarkAsUnverified.HasValue)
            body["autotranslate_mark_as_unverified"] = input.AutotranslateMarkAsUnverified.Value;

        if (input.AutotranslateUseMachineTranslation.HasValue)
            body["autotranslate_use_machine_translation"] = input.AutotranslateUseMachineTranslation.Value;

        if (input.AutotranslateUseTranslationMemory.HasValue)
            body["autotranslate_use_translation_memory"] = input.AutotranslateUseTranslationMemory.Value;


        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(body);

        var response = await Client.ExecuteWithErrorHandling<ProjectResponse>(request);
        return response;
    }
}