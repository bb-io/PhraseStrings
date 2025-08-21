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
        private JobActions _actions => new(InvocationContext);
        [TestMethod]
        public async Task SearchJobs_IsSuccess()
        {
            var response = await _actions.SearchJobs(
                new SearchJobsRequest { },
                new ProjectRequest {ProjectId= "52ea432ad1debbf8e09cdf344998167d" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreateJob_IsSuccess()
        {
            var response = await _actions.CreateJob(
                new CreateJobRequest { Name = "Testing job from locale callb", TranslationKeyIds = ["0c331ec27e910a1ed8c6af6cf2ba0c26", "7baf04c2118530edd7c024bc65f2f859"] },
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetJob_IsSuccess()
        {
            var response = await _actions.GetJob(
                new JobRequest { JobId= "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task StartJob_IsSuccess()
        {
            var response = await _actions.StartJob(
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                null);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddKeysToJob_IsSuccess()
        {
            var response = await _actions.AddKeysToJob(
                new JobRequest { JobId = "cae41b76eb9cdd623518f4d5effb2554" },
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" },
                new AddkeysToJobRequest { Keys = ["0c331ec27e910a1ed8c6af6cf2ba0c26", "7baf04c2118530edd7c024bc65f2f859"] });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddTargetLocaleToJob_IsSuccess()
        {
            var response = await _actions.AddTargetLocaleToJob(
                new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
                new JobRequest { JobId = "88bb8462fc7936b2b45a612c50866174" },
                new AddTargetLocaleToJobRequest { LocaleId = "df5447505e3e7a0b688c25a79a6770a7" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CompleteJob_IsSuccess()
        {
            var response = await _actions.CompleteJob(
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                null);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task ReopenJob_IsSuccess()
        {
            var response = await _actions.ReopenJob(
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                null);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task UpdateJob_IsSuccess()
        {
            var updatedJobName = $"Job name {Guid.NewGuid()}";

            var response = await _actions.UpdateJob(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" },
                new UpdateJobRequest
                {
                    Name = updatedJobName,
                    Briefing = "Automated test update",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    TicketUrl = "https://example.atlassian.net/browse/TEST-123"
                });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
         
            Console.WriteLine(json);
            Assert.IsTrue(response.Name.StartsWith(updatedJobName));
        }
    }
}
