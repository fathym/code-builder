using Microsoft.AspNetCore.Components;

namespace Fathym.LCU.IDE.Controls
{
    public class IDEStatusBarBase : ComponentBase
    {
        #region Properties
        /// <summary>
        /// TODO:  Change to Fathym.Status
        /// </summary>
        [Parameter]
        public virtual string? Status { get; set; }
        #endregion
    }
}