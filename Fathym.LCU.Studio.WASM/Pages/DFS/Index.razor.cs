using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazorise;
using BlazorPro.BlazorSize;
using Fathym.LCU.CodeCreator;
using Fathym.LCU.GrapesJS;
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

namespace Fathym.LCU.Studio.WASM.Pages.DFS
{
    public class IndexBase : ComponentBase
    {
        #region Fields
        protected List<IDEBarItemState> items;

        protected string backAction;
        #endregion

        #region MyRegion
        [CascadingParameter]
        public virtual AppState AppState { get; set; }
        #endregion

        #region Constructors
        public IndexBase()
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
                    Text = "Remote Files",
                    Icon = "fa-cube",
                    Position = IDEBarItemPositionTypes.End,
                    Items = new List<IDEBarItemState>()
                    {
                        new IDEBarItemState()
                        {
                            Text = "Google Drive",
                            Icon = "fa-brands fa-google-drive",
                            Path = "/studio/dfs/remotes/google/drive"
                        },
                        new IDEBarItemState()
                        {
                            Text = "S3 Bucket",
                            Icon = "fa-brands fa-aws",
                            Path = "/studio/dfs/remotes/aws/s3"
                        }
                    }
                },
                new IDEBarItemState()
                {
                    Text = "Settings",
                    Icon = "fa-gear",
                    //Icon = "fa-block-quote",
                    Path = "/studio/dfs/settings",
                    Position = IDEBarItemPositionTypes.End
                }
            };
        }
        #endregion
    }
}