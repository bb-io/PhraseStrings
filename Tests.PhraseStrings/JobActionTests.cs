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
                new CreateJobRequest { Name = "Testing job from locale callb", TranslationKeyIds = ["0c331ec27e910a1ed8c6af6cf2ba0c26", "7baf04c2118530edd7c024bc65f2f859"] },
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" });

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
                new JobRequest { JobId = "cae41b76eb9cdd623518f4d5effb2554" },
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" },
                new AddkeysToJobRequest { Keys = ["0c331ec27e910a1ed8c6af6cf2ba0c26", "7baf04c2118530edd7c024bc65f2f859"] });

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
