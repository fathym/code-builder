using System.Text.Json.Nodes;

namespace Fathym.LCU.Studio.WASM.State
{
    public class AppState
    {
        public virtual JsonObject? AppSettings { get; set; }

        public virtual bool SmallDown { get; set; }


        public virtual LCUStudioState? Studio { get; set; }
    }
}