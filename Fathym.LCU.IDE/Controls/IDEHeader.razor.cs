using Microsoft.AspNetCore.Components;

namespace Fathym.LCU.IDE.Controls
{
    public class IDEHeaderItemState
    {
        public virtual List<IDEHeaderItemState>? Items { get; set; }

        public virtual string? Path { get; set; }

        public virtual IDEHeaderItemPositionTypes Position { get; set; }

        public virtual string? Text { get; set; }
    }

    public enum IDEHeaderItemPositionTypes
    {
        Start,
        End
    }

    public class IDEHeaderBase : ComponentBase
    {
        #region Fields
        #endregion

        #region Properties
        [Parameter]
        public virtual RenderFragment? ChildContent { get; set; }

        [Parameter]
        public virtual List<IDEHeaderItemState>? Items { get; set; }

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
        protected virtual IDictionary<string, string> buildItemLinks(List<IDEHeaderItemState> items)
        {
            var itemLinks = new Dictionary<string, string>();

            Items?.ForEach(item =>
            {
                if (!String.IsNullOrEmpty(item.Path))
                    itemLinks[item.Path] = calculateAdjustedPath(item.Path);

                item.Items?.ForEach(subItem =>
                {
                    if (!String.IsNullOrEmpty(subItem.Path))
                        itemLinks[subItem.Path] = calculateAdjustedPath(subItem.Path);
                });
            });

            return itemLinks;
        }

        protected virtual string calculateAdjustedPath(string path)
        {
            if (path.StartsWith("/"))
                return $"./{PackagePath}{path}";
            else if (path.StartsWith("./"))
                return $"./{PackagePath}/{PluginPath}{path.Replace("./", "/")}";
            else
                return path;
        }
        #endregion
    }
}