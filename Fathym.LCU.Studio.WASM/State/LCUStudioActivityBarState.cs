using Fathym.LCU.IDE.Controls;
using System.Collections.Generic;

namespace Fathym.LCU.Studio.WASM.State
{
    public class LCUStudioActivityBarState
    {
        public virtual string? Icon { get; set; }

        public virtual List<IDEBarItemState>? Items { get; set; }

        public virtual string? Title { get; set; }
    }
}
