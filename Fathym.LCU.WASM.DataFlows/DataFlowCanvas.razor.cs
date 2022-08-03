using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fathym.LCU.WASM.DataFlows.JSPlumb;
using Fathym.LCU.WASM.DataFlows.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Fathym.LCU.WASM.DataFlows
{
    public class DataFlowCanvasBase : ComponentBase
    {
        #region Inject
        [Inject]
        protected JSPlumbInterop jsPlumbInterop { get; set; }
        #endregion

        #region Properties
        [Parameter]
        public virtual DataFlow DataFlow { get; set; }

        public virtual ElementReference DataFlowCanvas { get; set; }

        public virtual ElementReference El1 { get; set; }

        public virtual ElementReference El2 { get; set; }
        #endregion

        #region Constructors
        public DataFlowCanvasBase()
        {

        }
        #endregion

        #region Life Cycle
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var instance = new JSPlumbInstanceInterop(jsPlumbInterop, DataFlowCanvas, new MetadataModel());

            await instance.Connect(El1, El2);

            await base.OnAfterRenderAsync(firstRender);
        }
        #endregion

        #region API Methods

        #endregion

        #region Helpers

        #endregion
    }
}