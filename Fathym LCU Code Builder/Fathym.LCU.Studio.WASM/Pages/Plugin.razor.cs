using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazorise;
using BlazorPro.BlazorSize;
using Fathym.LCU.IDE.Controls;
using Fathym.LCU.Studio.WASM;
using Fathym.LCU.Studio.WASM.Shared;
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

namespace Fathym.LCU.Studio.WASM.Pages
{
    public class PluginBase : ComponentBase
    {
        #region Properties
        [Parameter]
        public virtual string? Package { get; set; }

        [Parameter]
        public virtual string? Plugin { get; set; }
        #endregion
    }
}