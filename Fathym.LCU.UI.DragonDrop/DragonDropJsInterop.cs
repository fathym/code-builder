using Microsoft.JSInterop;
using System.Text.Json.Nodes;

namespace Fathym.LCU.UI.DragonDrop
{
    public class DragonDropConfig
    {
        public virtual string? DragClass { get; set; }

        public virtual JsonNode? RootElement { get; set; }
    }

    public class DragonDropJsInterop : IAsyncDisposable
    {
        #region Fields
        protected readonly Lazy<Task<IJSObjectReference>> moduleTask;
        #endregion

        #region Constructors
        public DragonDropJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Fathym.LCU.UI.DragonDrop/fathym.lcu.ui.dragonDrop.js").AsTask());
        }
        #endregion

        #region Life Cycle
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;

                await module.DisposeAsync();
            }
        }
        #endregion

        #region API Methods
        public async ValueTask<string> Initialize(JsonObject config)
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<string>("initialize", config);
        }
        #endregion
    }
}