using Fathym.LCU.IDE.Controls;

namespace Fathym.LCU.Studio.WASM.State
{
    public class LCUStudioHeaderState
    {
        public virtual List<IDEBarItemState>? Items { get; set; }

        public virtual string? Title { get; set; }
    }
}
