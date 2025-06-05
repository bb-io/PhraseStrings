using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
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

            var response = await action.CreateJob(new CreateJobRequest {Name="Testing job 1" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

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

            var response = await action.AddKeysToJob(new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },  new AddkeysToJobRequest { Keys = [ "c9de884de06dafd683e65d3c2f2fa38c", "ec8c96f96bd5325b5a7c19559c5b9d94" ] });

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
