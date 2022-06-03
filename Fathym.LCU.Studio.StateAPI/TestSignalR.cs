using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fathym.LCU.Studio.StateAPI
{
    public class Counter : ICounter
    {
        #region Properties
        public virtual int Value { get; set; }
        #endregion

        #region API Methods
        public void Add(int amount)
        {
            this.Value += amount;
        }

        public Task Reset()
        {
            this.Value = 0;
            return Task.CompletedTask;
        }

        public Task<int> Get()
        {
            return Task.FromResult(this.Value);
        }

        public void Delete()
        {
            Entity.Current.DeleteState();
        }
        #endregion

        #region Life Cycle
        [FunctionName(nameof(Counter))]
        public static async Task Run([EntityTrigger] IDurableEntityContext ctx)
        {
            if (!ctx.HasState)
                ctx.SetState(new Counter());

            await ctx.DispatchAsync<Counter>();
        }
        #endregion
    }
    public class FathymStudioState : ServerlessHub
    {
        [FunctionName(nameof(Broadcast))]
        public virtual async Task Broadcast([SignalRTrigger] InvocationContext invocationContext, string message, ILogger logger)
        {
            //await Clients.All.SendAsync("send", new NewMessage(invocationContext, message));

            logger.LogInformation($"{invocationContext.ConnectionId} broadcast {message}");
        }

        #region Life Cycle
        [FunctionName("negotiate")]
        public virtual SignalRConnectionInfo Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req, ILogger logger)
        {
            var userId = req.Headers["x-ms-signalr-user-id"];

            logger.LogInformation($"Negotiating new connection for {userId}.");

            return Negotiate(userId: userId, claims: GetClaims(req.Headers["Authorization"]));
        }

        [FunctionName(nameof(OnConnected))]
        public virtual async Task OnConnected([SignalRTrigger] InvocationContext invocationContext, ILogger logger)
        {
            //await Clients.All.SendAsync(NewConnectionTarget, new NewConnection(invocationContext.ConnectionId));

            logger.LogInformation($"{invocationContext.ConnectionId} has connected");
        }

        [FunctionName(nameof(OnDisconnected))]
        public virtual void OnDisconnected([SignalRTrigger] InvocationContext invocationContext, ILogger logger)
        {
            logger.LogInformation($"{invocationContext.ConnectionId} has disconnected");
        }
        #endregion
    }
}