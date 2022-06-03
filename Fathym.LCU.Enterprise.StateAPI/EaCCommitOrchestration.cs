using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Fathym.LCU.Enterprise.StateAPI
{
    public static class EaCCommitOrchestration
    {
        #region Function Methods
        [FunctionName("EaCCommitOrchestration")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var commitReq = context.GetInput<CommitEnterpriseAsCodeRequest>();

            var outputs = new List<string>();

            var tasks = new List<Task>();

            var isRunning = true;

            var entityId = new EntityId(nameof(EaCState), commitReq.EaC.EnterpriseLookup);

            var eacState = context.CreateEntityProxy<IEaCState>(entityId);

            tasks.Add(Task.Run(async () =>
            {
                // Replace "hello" with the name of your Durable Activity Function.
                outputs.Add(await context.CallActivityAsync<string>("EaCCommitOrchestration_Hello", "Tokyo"));

                context.SetCustomStatus(new Status()
                {
                    Message = "Tokyo",
                    Code = 0
                });

                var deadline = context.CurrentUtcDateTime.Add(TimeSpan.FromSeconds(30));

                await context.CreateTimer(deadline, CancellationToken.None);

                outputs.Add(await context.CallActivityAsync<string>("EaCCommitOrchestration_Hello", "Seattle"));

                context.SetCustomStatus(new Status()
                {
                    Message = "Seattle",
                    Code = 0
                });

                deadline = context.CurrentUtcDateTime.Add(TimeSpan.FromSeconds(30));

                await context.CreateTimer(deadline, CancellationToken.None);

                outputs.Add(await context.CallActivityAsync<string>("EaCCommitOrchestration_Hello", "London"));

                context.SetCustomStatus(new Status()
                {
                    Message = "London",
                    Code = 0
                });

                deadline = context.CurrentUtcDateTime.Add(TimeSpan.FromSeconds(30));

                await context.CreateTimer(deadline, CancellationToken.None);

                if (!context.IsReplaying)
                    isRunning = false;
            }));

            await Task.WhenAll(tasks);

            await parallelRun(async () =>
            {
                await eacState.LoadCommitStatus();

                var nextCheckpoint = context.CurrentUtcDateTime.AddSeconds(15);

                return context.CreateTimer(nextCheckpoint, CancellationToken.None);
            });

            await eacState.CommitComplete(Status.Success);

            return outputs;
        }

        private static async Task parallelRun(params Func<Task<Task>>[] taskLoaders)
        {
            var tasks = new List<Task>();

            await taskLoaders.Each(async tl =>
            {
                tasks.Add(tl());
            });

            await Task.WhenAll(tasks);
        }

        [FunctionName("EaCCommitOrchestration_Hello")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            //var eacResp = await eacSvc.Commit(request);
            return $"Hello {name}!";
        }
        #endregion

        #region Helpers
        #endregion
    }
}