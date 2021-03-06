using Microsoft.JSInterop;
using System.Text.Json.Nodes;

namespace Fathym.LCU.Utils
{
    public class ConfigUtilsJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ConfigUtilsJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Fathym.LCU/configUtils.js").AsTask());
        }

        public async ValueTask<string> InjectHeadScript(string source, string id = null, bool asContent = false)
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<string>("injectHeadScript", source, id, asContent);
        }

        public async ValueTask<JsonObject> LoadLCUSettings()
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<JsonObject>("loadLcuSettings");
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