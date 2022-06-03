using System;
using System.IO;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using LCU.Personas.Registry.EaC;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Host;

namespace Fathym.LCU.Enterprise.StateAPI
{

    public interface IEaCState
    {
        Task<Status> Commit(EaCStateCommitRequest request);

        Task CommitComplete(Status success);

        Task Load(ExportEnterpriseAsCodeRequest request);

        Task LoadCommitStatus();

        Task<Status> ResetState();

        Task<Status> SetCommitManagement(CheckStatusPayload management);
    }
    public class EaCState : LCUState, IEaCState
    {
        #region Fields
        protected readonly IHttpClientFactory clientFactory;

        protected readonly IEnterprisesAsCodeService eacSvc;

        protected string entLookup
        {
            get { return context.EntityKey; }
        }
        #endregion

        #region Properties
        public virtual bool Committing { get; set; }

        public virtual CheckStatusPayload CommitManagement { get; set; }

        public virtual JsonObject CommitStatus { get; set; }

        [JsonPropertyName("eac")]
        public virtual EnterpriseAsCode EaC { get; set; }

        public virtual Status EaCStatus { get; set; }
        #endregion

        #region Constructors
        public EaCState(IEnterprisesAsCodeService eacSvc, IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;

            this.eacSvc = eacSvc;
        }
        #endregion

        #region API Methods
        public virtual async Task<Status> Commit(EaCStateCommitRequest request)
        {
            if (request.CurrentStatus == null ||
                request.CurrentStatus.RuntimeStatus == OrchestrationRuntimeStatus.Completed ||
                request.CurrentStatus.RuntimeStatus == OrchestrationRuntimeStatus.Failed ||
                request.CurrentStatus.RuntimeStatus == OrchestrationRuntimeStatus.Terminated)
            {
                var instanceId = context.StartNewOrchestration("EaCCommitOrchestration", request.Commit,
                    instanceId: entLookup);

                Committing = true;

                return Status.Success;
            }
            else
                return Status.Conflict.Clone($"An EaCCommitOrchestration with ID '{entLookup}' already exists.");
        }

        public virtual async Task CommitComplete(Status status)
        {
            Committing = false;
        }

        public virtual async Task Load(ExportEnterpriseAsCodeRequest request)
        {
            var eacResp = await eacSvc.Export(request, entLookup);

            if (eacResp.Status)
            {
                EaC = eacResp.Model;
            }

            EaCStatus = eacResp.Status;
        }
        
        public virtual async Task LoadCommitStatus()
        {
            if (CommitManagement != null)
            {
                var statusClient = clientFactory.CreateClient();
                statusClient.BaseAddress = new Uri(CommitManagement.StatusQueryGetUri);

                var resp = await statusClient.GetStringAsync("");

                CommitStatus = resp.FromJSON<JsonObject>();
            }
        }

        public virtual async Task<Status> ResetState()
        {
            context.DeleteState();

            EaCStatus = Status.Initialized;

            return Status.Success;
        }

        public virtual async Task<Status> SetCommitManagement(CheckStatusPayload payload)
        {
            CommitManagement = payload;

            return Status.Success;
        }
        #endregion

        #region Life Cycle
        [FunctionName(nameof(EaCState))]
        public async Task Run([EntityTrigger] IDurableEntityContext ctx)
        {
            //if (!ctx.HasState)
            //    ctx.SetState(new EaCState());

            await ctx.DispatchAsync<EaCState>();
        }
        #endregion
    }
}