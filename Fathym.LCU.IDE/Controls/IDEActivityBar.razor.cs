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
        public virtual string? BackAction { get; set; }

        [Parameter]
        public virtual Background Background { get; set; }

        [Parameter]
        public virtual BarCollapseMode CollapseMode { get; set; }

        [Parameter]
        public virtual string? Divider { get; set; }

        [Parameter]
        public virtual List<IDEBarItemState>? Items { get; set; }

        public virtual Bar? Sidebar { get; set; }

        [Parameter]
        public virtual ThemeContrast ThemeContrast { get; set; }

        [Parameter]
        public virtual string? Title { get; set; }

        [Parameter]
        public virtual TextColor TitleColor { get; set; }

        [Parameter]
        public virtual string? TitleIcon { get; set; }

        [Parameter]
        public virtual string? TitleLink { get; set; }
        #endregion

        #region Constructors
        public IDEActivityBarBase()
        {
            Background = Background.Default;

            CollapseMode = BarCollapseMode.Small;

            ThemeContrast = ThemeContrast.Dark;

            TitleColor = TextColor.Default;
        }
        #endregion
    }
}