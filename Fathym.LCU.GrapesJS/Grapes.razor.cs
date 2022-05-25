using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fathym.LCU.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Fathym.LCU.GrapesJS
{
    public class GrapesBase : ComponentBase
    {
        #region Inject
        [Inject]
        protected ConfigUtilsJsInterop? configUtils { get; set; }

        [Inject]
        protected FathymLCUGrapesInterop? grapesInterop { get; set; }
        #endregion

        #region Properties
        [Parameter]
        public virtual GrapesJSConfig Config { get; set; }

        [Parameter]
        public virtual string ID { get; set; }
        #endregion

        #region Constructors
        public GrapesBase()
        {
            ID = "gjs";

            Config = new GrapesJSConfig()
            {
                Container = $"#{ID}"
            };

        }
        #endregion

        #region Life Cycle
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await grapesInterop!.Initialize(Config);

            await base.OnParametersSetAsync();
        }
        #endregion
    }
}