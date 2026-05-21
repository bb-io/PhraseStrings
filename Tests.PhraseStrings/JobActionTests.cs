using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class JobActionTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task SearchJobs_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.SearchJobs(
                new SearchJobsRequest { },
                new ProjectRequest {ProjectId= "52ea432ad1debbf8e09cdf344998167d" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task CreateJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.CreateJob(
                new CreateJobRequest
                {
                    Name = "Testing job from locale callb",
                    TargetLocaleIds = ["df5447505e3e7a0b688c25a79a6770a7"],
                    TargetLocaleCodes = ["nl-NL"]
                },
                new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task GetJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.GetJob(
                new JobRequest { JobId= "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task StartJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.StartJob(
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                null);

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task AddKeysToJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.AddKeysToJob(
                new JobRequest { JobId = "88bb8462fc7936b2b45a612c50866174" },
                new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
                new AddkeysToJobRequest { KeyNames = ["dashboard_welcome_message"] });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task AddTargetLocaleToJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.AddTargetLocaleToJob(
                new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
                new JobRequest { JobId = "88bb8462fc7936b2b45a612c50866174" },
                new AddTargetLocaleToJobRequest { LocaleId = "df5447505e3e7a0b688c25a79a6770a7" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task CompleteJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.CompleteJob(
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                null);

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task ReopenJob_IsSuccess(InvocationContext context)
        {
            var actions = new JobActions(context);
            var response = await actions.ReopenJob(
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                null);

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task UpdateJob_IsSuccess(InvocationContext context)
        {
            var updatedJobName = $"Job name {Guid.NewGuid()}";

            var actions = new JobActions(context);
            var response = await actions.UpdateJob(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new UpdateJobRequest
                {
                    Name = updatedJobName,
                    Briefing = "Automated test update",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    TicketUrl = "https://example.atlassian.net/browse/TEST-123"
                });

            PrintResult(response);
            Assert.IsTrue(response.Name.StartsWith(updatedJobName));
        }
    }
}
