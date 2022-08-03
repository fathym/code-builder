using LCU.Personas.StateAPI.StateServices;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace Fathym.LCU.Social
{
    public class SocialStateService : StateService
    {
        #region Constructors
        public SocialStateService(string url, HttpTransportType transport)
            : base(url, transport)
        { }
        #endregion

        #region API Methods
        public virtual Task Commit()
        {
            return Hub.InvokeAsync("StudioStateHub_ProcessEaC");
        }
        #endregion
    }
}