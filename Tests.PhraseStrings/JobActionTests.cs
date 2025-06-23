using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks;
using Apps.PhraseStrings.Webhooks.Base;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class JobActionTests : TestBase
    {
        [TestMethod]
        public async Task SearchJobs_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.SearchJobs(new SearchJobsRequest { },
                new ProjectRequest {ProjectId= "52ea432ad1debbf8e09cdf344998167d" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreateJob_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.CreateJob(new CreateJobRequest {Name="Testing job from locale callb", TranslationKeyIds= ["0c331ec27e910a1ed8c6af6cf2ba0c26", "7baf04c2118530edd7c024bc65f2f859"] },
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetJob_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.GetJob(new JobRequest { JobId= "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task StartJob_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.StartJob(new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, null);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddKeysToJob_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.AddKeysToJob(new JobRequest { JobId = "cae41b76eb9cdd623518f4d5effb2554" },
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" },  new AddkeysToJobRequest { Keys = ["0c331ec27e910a1ed8c6af6cf2ba0c26", "7baf04c2118530edd7c024bc65f2f859"] });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CompleteJob_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.CompleteJob(new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, null);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task ReopenJob_IsSuccess()
        {
            var action = new JobActions(InvocationContext, FileManager);

            var response = await action.ReopenJob(new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, null);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
