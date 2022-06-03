using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Fathym.LCU.Studio.StateAPI
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName("Function1_Hello")]
        public static async Task<string> SayHello([ActivityTrigger] string name,
            [DurableClient] IDurableEntityClient client, ILogger log)
        {
            var entityId = new EntityId("Counter", "Test6");

            var count = await client.ReadEntityStateAsync<Counter>(entityId);

            await client.SignalEntityAsync<ICounter>(entityId, prx => prx.Add(count.EntityState?.Value ?? 1));

            count = await client.ReadEntityStateAsync<Counter>(entityId);

            log.LogInformation($"Saying hello to {name}:{count.EntityState?.Value}.");

            return $"Hello {name}:{count.EntityState?.Value}!";
        }

        [FunctionName("GetState")]
        public static async Task<Counter> GetState(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client, ILogger log)
        {
            var entityId = new EntityId("Counter", "Test6");

            var count = await client.ReadEntityStateAsync<Counter>(entityId);

            return count.EntityState;
        }

        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}