using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Fathym.LCU.UI.Controls
{
    public class LoadingBase : ComponentBase
    {
        #region Fields
        #endregion

        #region Properties
        [Parameter]
        public virtual string Size { get; set; }
        #endregion

        #region Constructors
        public LoadingBase()
        {
            Size = "150px";
        }
        #endregion

        #region Life Cycle
        #endregion

        #region Helpers
        #endregion
    }
}