using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using LCU.Personas.Registry.EaC;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Fathym.LCU.Enterprise.StateAPI
{
    public class EaCStateAPIs
    {
        #region Fields
        #endregion

        #region Properties
        public virtual EnterpriseAsCode EaC { get; set; }
        #endregion

        #region Constructors
        public EaCStateAPIs()
        { }
        #endregion

        #region HTTP APIs
        #region Routes
        private const string commitRoute = $"{nameof(EaCState)}/" + "{entLookup}";

        private const string getStateRoute = $"{nameof(EaCState)}/" + "{entLookup}";

        private const string loadEaCStateRoute = $"{nameof(EaCState)}/" + "{entLookup}/load/eac";

        private const string resetStateRoute = $"{nameof(EaCState)}/" + "{entLookup}";
        #endregion

        [FunctionName($"{nameof(EaCState)}_Commit")]
        [return: Queue("eacstate-commit", Connection = "LCU_STORAGE_QUEUE_CONNECTION")]
        public virtual async Task<string> EaCStateCommitAPI(ILogger log,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = commitRoute)] HttpRequestMessage req,
            string entLookup)
        {
            var content = await req.Content.ReadAsStringAsync();

            var eac = content?.FromJSON<EnterpriseAsCode>() ?? new EnterpriseAsCode();

            eac.EnterpriseLookup = entLookup;

            var commitReq = new CommitEnterpriseAsCodeRequest()
            {
                EaC = eac,
                Removals = false,
                Username = req.GetCurrentUser()
            };

            return commitReq?.ToJSON() ?? new { }.ToJSON();
        }

        [FunctionName($"{nameof(EaCState)}_CommitHandle")]
        public virtual async Task EaCStateCommitHandleAPI(ILogger logger,
            [QueueTrigger("eacstate-commit", Connection = "LCU_STORAGE_QUEUE_CONNECTION")] string commitReqMsg,
            [DurableClient] IDurableEntityClient client,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            var commitReq = commitReqMsg.FromJSON<CommitEnterpriseAsCodeRequest>();

            if (!commitReq.EaC.EnterpriseLookup.IsNullOrEmpty())
            {
                logger.LogInformation($"Preparing to commit enterprise {commitReq.EaC.EnterpriseLookup}");

                var existingInstance = await starter.GetStatusAsync(commitReq.EaC.EnterpriseLookup);

                var entityId = new EntityId(nameof(EaCState), commitReq.EaC.EnterpriseLookup);

                await client.SignalEntityAsync<IEaCState>(entityId,
                    async prx => await prx.Commit(new EaCStateCommitRequest()
                    {
                        Commit = commitReq,
                        CurrentStatus = existingInstance
                    }));

                logger.LogInformation($"Started commit orchestration for enterprise {commitReq.EaC.EnterpriseLookup}");

                var checkStatusResp = starter.CreateCheckStatusResponse(new HttpRequestMessage(), 
                    commitReq.EaC.EnterpriseLookup);

                var checkStatusStr = await checkStatusResp.Content.ReadAsStringAsync();

                var checkStatus = checkStatusStr.FromJSON<CheckStatusPayload>();

                await client.SignalEntityAsync<IEaCState>(entityId, 
                    async prx => await prx.SetCommitManagement(checkStatus));

                logger.LogInformation($"Set check status values on enterprise {commitReq.EaC.EnterpriseLookup}");
            }
            else
            {
                logger.LogWarning($"Incorrect structure for enterprise commit:\r\n{commitReqMsg}");

            }
        }

        [FunctionName($"{nameof(EaCState)}_GetState")]
        public virtual async Task<EaCState> EaCStateGetStateAPI(ILogger log,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = getStateRoute)] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client, string entLookup)
        {
            var entityId = new EntityId(nameof(EaCState), entLookup);

            var state = await client.ReadEntityStateAsync<EaCState>(entityId);

            return state.EntityState;
        }

        [FunctionName($"{nameof(EaCState)}_LoadEaC")]
        public virtual async Task<Status> EaCStateLoadStateAPI(ILogger log,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = loadEaCStateRoute)] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client, string entLookup)
        {
            var entityId = new EntityId(nameof(EaCState), entLookup);

            await client.SignalEntityAsync<IEaCState>(entityId, async prx => await prx.Load(new ExportEnterpriseAsCodeRequest()
            {
                ForceCache = true
            }));

            return Status.Success;
        }

        [FunctionName($"{nameof(EaCState)}_ResetState")]
        public virtual async Task<Status> EaCStateResetStateAPI(ILogger log,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = resetStateRoute)] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client, string entLookup)
        {
            var entityId = new EntityId(nameof(EaCState), entLookup);

            await client.SignalEntityAsync<IEaCState>(entityId, async prx => await prx.ResetState());

            await client.SignalEntityAsync<IEaCState>(entityId, async prx => await prx.Load(new ExportEnterpriseAsCodeRequest()
            {
                ForceCache = true
            }));

            return Status.Success;
        }
        #endregion
    }
}