using Microsoft.JSInterop;

namespace Fathym.LCU.GrapesJS
{
    public class FathymLCUGrapesInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public FathymLCUGrapesInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Fathym.LCU.GrapesJS/fathym.lcu.grapesjs.js").AsTask());
        }

        public async ValueTask<string> Initialize(GrapesJSConfig grapesJsConfig)
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<string>("initializeGrapesJs", grapesJsConfig);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}