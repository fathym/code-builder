using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazorise;
using BlazorPro.BlazorSize;
using Fathym.LCU.IDE.Controls;
using Fathym.LCU.UI.Controls;
using Fathym.LCU.Utils;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;
using Fathym.LCU.Studio.WASM.State;

namespace Fathym.LCU.Studio.WASM.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        #region Fields
        protected IDEActivityBarBase activityBar;

        protected LCUStudioState studioState;
        #endregion

        #region Properties
        [CascadingParameter]
        public virtual AppState AppState { get; set; }
        #endregion

        #region Constructors
        public MainLayoutBase()
        { }
        #endregion

        #region Life Cycle
        protected override void OnParametersSet()
        {
            studioState = AppState?.Studio ?? new LCUStudioState();

            base.OnParametersSet();
        }
        #endregion
    }
}
