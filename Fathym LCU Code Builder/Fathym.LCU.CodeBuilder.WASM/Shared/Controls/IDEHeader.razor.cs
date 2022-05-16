using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazorise;
using Fathym.LCU.CodeBuilder.WASM;
using Fathym.LCU.CodeBuilder.WASM.Shared;
using Fathym.LCU.CodeBuilder.WASM.Shared.Controls;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;

namespace Fathym.LCU.CodeBuilder.WASM.Shared.Controls
{
    public class IDEHeaderBase : ComponentBase
    {
        #region Properties
        [Parameter]
        public virtual RenderFragment? ChildContent { get; set; }

        [Parameter]
        public virtual string? Title { get; set; }
        #endregion
    }
}