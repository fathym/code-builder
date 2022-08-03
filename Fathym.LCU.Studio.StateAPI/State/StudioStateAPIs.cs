using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using LCU.Personas.Enterprises.Architect.State;
using LCU.Personas.Enterprises.Architect.StateServices;
using LCU.Personas.StateAPI;
using LCU.Personas.StateAPI.Durable;
using LCU.Personas.StateAPI.StateServices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fathym.LCU.Studio.StateAPI.State
{
    public class StudioStateAPIs : LCUStateAPIs<StudioStateEntity>
    {
        #region Constructors
        public StudioStateAPIs(ILogger<StudioStateAPIs> logger)
            : base(logger)
        { }
        #endregion

        #region API Methods
        [FunctionName($"{nameof(StudioStateAPIs)}_{nameof(HandleEaCUpdate)}")]
        public virtual async Task HandleEaCUpdate(ILogger log,
            [StateServiceTrigger(URL = "EAC_STATE_HUB_URL")] string eacStateStr,
            [StateService(URL = "EAC_STATE_HUB_URL")] EaCStateService eacStateSvc,
            [DurableClient] IDurableEntityClient client,
            [SignalR(HubName = "StudioStateHub")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var eacState = eacStateStr.FromJSON<EaCState>();

            var entityId = new EntityId(nameof(StudioStateEntity), eacState.EaC.EnterpriseLookup);

            await client.SignalEntityAsync<IStudioState>(entityId, (studioState) => studioState.ProcessEaC(eacState));

            await signalStateUpdate(client, signalRMessages, eacState.EaC.EnterpriseLookup);
        }
        #endregion
    }
}
