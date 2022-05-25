using Microsoft.AspNetCore.Components;

namespace Fathym.LCU.UI.DragonDrop
{
    public class DragonDropContainerBase<TDragonDropItem> : ComponentBase
    {
        #region Properties
        [Parameter]
        public virtual Func<TDragonDropItem, Task>? OnDrop { get; set; }
        #endregion

        #region Constructors
        public DragonDropContainerBase()
        {

        }
        #endregion

        #region Life Cycle

        #endregion

        #region API Methods
        public virtual async Task HandleDrop(TDragonDropItem item)
        {
            if (OnDrop != null)
                await OnDrop(item);
        }
        #endregion
    }
}