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

        #region Fields
        protected Lazy<ElementReference> dataFlowCanvas;

        protected JSPlumbInstanceInterop jsPlumbInstance;
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
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();

        //    jsPlumbInstance = new JSPlumbInstanceInterop(jsPlumbInterop, new(() => DataFlowCanvas), 
        //        new MetadataModel());
        //}
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    await base.OnAfterRenderAsync(firstRender);

        //    await jsPlumbInstance.Connect(El1, El2);
        //}
        #endregion

        #region API Methods

        #endregion

        #region Helpers

        #endregion
    }
}