using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fathym.LCU.WASM.DataFlows.JSPlumb
{
    public class JSPlumbInterop : IAsyncDisposable
    {
        #region Fields
        protected readonly Lazy<Task<IJSObjectReference>> moduleTask;
        #endregion

        #region Constructors
        public JSPlumbInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Fathym.LCU.WASM.DataFlows/jsPlumbInterop.js").AsTask());
        }
        #endregion

        #region Life Cycle
        public virtual async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;

                await module.DisposeAsync();
            }
        }
        #endregion

        #region API Methods
        public virtual async ValueTask<IJSObjectReference> Create(ElementReference container, MetadataModel defaults)
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<IJSObjectReference>("JSPlumbInterop.Create", container, defaults);
        }

        public virtual async Task RegisterHandlers(JSPlumbInstanceInterop instanceInterop, IJSObjectReference instance)
        {
            var module = await moduleTask.Value;

            await module.InvokeVoidAsync("JSPlumbInterop.RegisterHandlers", DotNetObjectReference.Create(instanceInterop), instance);
        }
        #endregion
    }
}