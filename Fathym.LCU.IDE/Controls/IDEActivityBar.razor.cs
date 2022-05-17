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
using Microsoft.AspNetCore.Components.Web;

namespace Fathym.LCU.IDE.Controls
{
    public class IDEActivityBarBase : ComponentBase
    {
        #region Properties
        [Parameter]
        public virtual string? Title { get; set; }
        #endregion
    }
}