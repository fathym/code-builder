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

namespace Fathym.LCU.Studio.WASM.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        #region Fields
        protected List<IDEHeaderItemState> headerItems;
        #endregion

        #region Constructors
        public MainLayoutBase()
        {
            headerItems = new List<IDEHeaderItemState>()
            {
                new IDEHeaderItemState()
                {
                    Text = "Getting Started",
                    Position = IDEHeaderItemPositionTypes.Start,
                    Items = new List<IDEHeaderItemState>()
                    {
                        new IDEHeaderItemState()
                        {
                            Text = "Walkthrough of Studio Features"
                        },
                        new IDEHeaderItemState(),
                        new IDEHeaderItemState()
                        {
                            Text = "Coming Soon"
                        }
                    }
                },
                new IDEHeaderItemState()
                {
                    Text = "Documentation",
                    Path = "https://www.fathym.com/docs"
                },
                new IDEHeaderItemState()
                {
                    Text = "Hey",
                    Path = "./there",
                    Position = IDEHeaderItemPositionTypes.End
                }
            };
        }
        #endregion
    }
}