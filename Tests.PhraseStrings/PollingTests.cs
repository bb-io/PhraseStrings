using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class PollingTests : TestBaseMultipleConnections
    {
        private const string ProjectId = "52ea432ad1debbf8e09cdf344998167d";
        private const string JobId = "616f70f52101f2e219f0ef8192320871";

        [TestMethod, ContextDataSource]
        public async Task OnRepoSyncFailure_IsSuccess(InvocationContext context)
        {
            var polling = new PollingList(context);

            var request = new PollingEventRequest<DateMemory>
            {
                //Memory = new DateMemory { LastInteractionDate = DateTime.UtcNow.AddDays(-20) }
            };
            var input = new RepoSyncRequest
            {
                AccountId = "dd76e8ff50005091bd5c1757c2b6e893"
            };
            var response = await polling.OnRepoSyncFailure(request, input);

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task OnJobCompletedPolling_WithoutMemory_IsSuccess(InvocationContext context)
        {
            var polling = new PollingList(context);
            var pollingStartedAt = DateTime.UtcNow;

            var response = await polling.OnJobCompletedPolling(
                new PollingEventRequest<JobStatePollingMemory>(),
                new ProjectRequest { ProjectId = ProjectId },
                new JobRequest { JobId = JobId });

            PrintResult(response);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Memory, "Memory must be advanced on every poll.");
            Assert.IsTrue(response.Memory!.LastPollingTime >= pollingStartedAt, "Memory must store the current polling time.");

            var isCompleted = string.Equals(response.Memory.LastState, "completed", StringComparison.OrdinalIgnoreCase);
            Assert.AreEqual(isCompleted, response.FlyBird, "The bird must fly only when the job is completed.");

            if (response.FlyBird)
            {
                Assert.IsNotNull(response.Result);
                Assert.AreEqual(JobId, response.Result!.Job?.Id);
                Assert.AreEqual(ProjectId, response.Result.Project?.Id);
                Assert.AreEqual("jobs:complete", response.Result.Event);
                Assert.IsTrue(
                    string.Equals(response.Result.Job?.State, "completed", StringComparison.OrdinalIgnoreCase),
                    "The payload must carry the completed job state.");
            }
            else
            {
                Assert.IsNull(response.Result, "No payload must be returned when the bird does not fly.");
            }
        }

        [TestMethod, ContextDataSource]
        public async Task OnJobCompletedPolling_WithNotCompletedMemory_IsSuccess(InvocationContext context)
        {
            var polling = new PollingList(context);

            var response = await polling.OnJobCompletedPolling(
                new PollingEventRequest<JobStatePollingMemory>
                {
                    Memory = new JobStatePollingMemory
                    {
                        LastPollingTime = DateTime.UtcNow.AddHours(-1),
                        LastState = "in_progress"
                    }
                },
                new ProjectRequest { ProjectId = ProjectId },
                new JobRequest { JobId = JobId });

            PrintResult(response);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Memory);

            var isCompleted = string.Equals(response.Memory!.LastState, "completed", StringComparison.OrdinalIgnoreCase);
            Assert.AreEqual(isCompleted, response.FlyBird, "The bird must fly only when the job reached the completed state.");
            Assert.AreEqual(response.FlyBird, response.Result is not null, "A payload must be present exactly when the bird flies.");
        }

        [TestMethod, ContextDataSource]
        public async Task OnJobCompletedPolling_WhenCompletionAlreadyReported_DoesNotFly(InvocationContext context)
        {
            var polling = new PollingList(context);

            var response = await polling.OnJobCompletedPolling(
                new PollingEventRequest<JobStatePollingMemory>
                {
                    Memory = new JobStatePollingMemory
                    {
                        LastPollingTime = DateTime.UtcNow.AddHours(-1),
                        LastState = "completed"
                    }
                },
                new ProjectRequest { ProjectId = ProjectId },
                new JobRequest { JobId = JobId });

            PrintResult(response);

            Assert.IsFalse(response.FlyBird, "The bird must not fly twice for the same completion.");
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Memory);
        }

        [TestMethod, ContextDataSource]
        public async Task OnJobCompletedPolling_WithoutProjectId_ThrowsMisconfiguration(InvocationContext context)
        {
            var polling = new PollingList(context);

            var exception = await Assert.ThrowsExceptionAsync<PluginMisconfigurationException>(() =>
                polling.OnJobCompletedPolling(
                    new PollingEventRequest<JobStatePollingMemory>(),
                    new ProjectRequest { ProjectId = string.Empty },
                    new JobRequest { JobId = JobId }));

            TestContext?.WriteLine(exception.Message);
            Assert.IsTrue(exception.Message.Contains("Project ID", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod, ContextDataSource]
        public async Task OnJobCompletedPolling_WithoutJobId_ThrowsMisconfiguration(InvocationContext context)
        {
            var polling = new PollingList(context);

            var exception = await Assert.ThrowsExceptionAsync<PluginMisconfigurationException>(() =>
                polling.OnJobCompletedPolling(
                    new PollingEventRequest<JobStatePollingMemory>(),
                    new ProjectRequest { ProjectId = ProjectId },
                    new JobRequest { JobId = string.Empty }));

            TestContext?.WriteLine(exception.Message);
            Assert.IsTrue(exception.Message.Contains("Job ID", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod, ContextDataSource]
        public async Task OnJobCompletedPolling_WithNonExistingJob_ThrowsReadableError(InvocationContext context)
        {
            var polling = new PollingList(context);

            var exception = await Assert.ThrowsExceptionAsync<PluginApplicationException>(() =>
                polling.OnJobCompletedPolling(
                    new PollingEventRequest<JobStatePollingMemory>(),
                    new ProjectRequest { ProjectId = ProjectId },
                    new JobRequest { JobId = "00000000000000000000000000000000" }));

            TestContext?.WriteLine(exception.Message);
            Assert.IsFalse(string.IsNullOrWhiteSpace(exception.Message));
        }
    }
}
