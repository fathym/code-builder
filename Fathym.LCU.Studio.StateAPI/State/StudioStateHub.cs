﻿using LCU.Personas.Enterprises.Architect.State;
using LCU.Personas.StateAPI;
using LCU.Personas.StateAPI.Durable;
using LCU.Personas.StateAPI.StateServices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fathym.LCU.Studio.StateAPI.State
{
    public class StudioStateHub : LCUStateHub<StudioStateEntity>
    {
        #region Life Cycle
        [FunctionName("negotiate")]
        public virtual Task<SignalRConnectionInfo> Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestMessage req)
        {
            return negotiate(req);
        }

        [FunctionName(nameof(OnConnected))]
        public virtual async Task OnConnected([SignalRTrigger] InvocationContext invocationContext,
            [StateService(URL = "EAC_STATE_HUB_URL")] EaCStateService eacStateSvc,
            [DurableClient] IDurableEntityClient client, ILogger logger)
        {
            await connected(invocationContext, client, logger);

            //  Attach listener to user current Enterprise EaCState (this will acquire the current state as well, this will
            //      process current EaCState into studio state)
        }

        ////[FunctionAuthorize]
        //[FunctionName($"{nameof(StudioStateHub_ProcessEaC)}")]
        //[return: Queue("studiostate-processeac", Connection = "LCU_STORAGE_QUEUE_CONNECTION")]
        //public virtual async Task<string> StudioStateHub_ProcessEaC([SignalRTrigger] InvocationContext invocationContext,
        //    [DurableClient] IDurableEntityClient client, EaCState eacState, ILogger logger)
        //{
        //    var entLookup = invocationContext.Claims["lcu-ent-lookup"];

        //    var username = invocationContext.UserId;

        //    await loadAndUpdateState(client, entLookup);

        //    return eacState.ToJSON() ?? new { }.ToJSON();
        //}
        #endregion

        #region Helpers
        protected override async Task handleStateEmpty(IDurableEntityClient client, EntityId entityId)
        {
            //await client.SignalEntityAsync<IStudioState>(entityId, (eacState) => eacState.Load(new ExportEnterpriseAsCodeRequest()
            //{
            //    ForceCache = true
            //}));
        }
        #endregion
    }
}
