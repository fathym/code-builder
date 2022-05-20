using Blazorise;
using Microsoft.AspNetCore.Components;

namespace Fathym.LCU.IDE.Controls
{
    public class IDEHeaderBase : ComponentBase
    {
        #region Fields
        #endregion

        #region Properties
        [Parameter]
        public virtual Bar? BarRef { get; set; }

        [Parameter]
        public virtual RenderFragment? ChildContent { get; set; }

        [Parameter]
        public virtual List<IDEBarItemState>? Items { get; set; }

        [Parameter]
        public virtual string? PackagePath { get; set; }

        [Parameter]
        public virtual string? PluginPath { get; set; }

        [Parameter]
        public virtual string? Title { get; set; }
        #endregion

        #region Constructors
        public IDEHeaderBase()
        { }
        #endregion

        #region Life Cycle
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }
        #endregion

        #region Helpers
        #endregion
    }
}