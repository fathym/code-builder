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
using Fathym.LCU.Studio.WASM.State;

namespace Fathym.LCU.Studio.WASM.Pages
{
    public class MyStudioBase : ComponentBase
    {
        #region Fields
        protected List<IDEBarItemState> items;

        protected string backAction;

        protected Bar sidebar;
        #endregion

        #region MyRegion
        [CascadingParameter]
        public virtual AppState AppState { get; set; }
        #endregion

        #region Constructors
        public MyStudioBase()
        {

        }
        #endregion

        #region Life Cycle
        protected override async Task OnInitializedAsync()
        {
            await retrieveEaC();

            await base.OnInitializedAsync();
        }
        #endregion

        #region Helpers
        protected virtual async Task retrieveEaC()
        {
            items = new List<IDEBarItemState>()
            {
                new IDEBarItemState()
                {
                    Text = "Design Blocks",
                    Icon = "fas fa-cube",
                    Path = "/mystudio/design-blocks",
                    Position = IDEBarItemPositionTypes.End,
                    Items = new List<IDEBarItemState>()
                    {
                        new IDEBarItemState()
                        {
                            Text = "Action",
                            Icon = "fas fa-link",
                            Path = "/mystudio/design-blocks/action"
                        },
                        new IDEBarItemState()
                        {
                            Text = "Text",
                            Icon = "fas fa-text",
                            Path = "/mystudio/design-blocks/text"
                        }
                    }
                },
                new IDEBarItemState()
                {
                    Text = "Components",
                    Icon = "fas fa-cubes",
                    Path = "/mystudio/components",
                    Position = IDEBarItemPositionTypes.End
                },
                new IDEBarItemState()
                {
                    Text = "Templates",
                    Icon = "fas fa-cubes-stacked",
                    //Icon = "fa-block-quote",
                    Path = "/mystudio/templates",
                    Position = IDEBarItemPositionTypes.End
                }
            };
        }
        #endregion
    }
}