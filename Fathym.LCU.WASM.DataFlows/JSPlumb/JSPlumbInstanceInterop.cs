using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fathym.LCU.WASM.DataFlows.JSPlumb
{
    public class JSPlumbInstanceInterop : IAsyncDisposable
    {
        #region Fields
        protected readonly Lazy<Task<IJSObjectReference>> instanceTask;
        #endregion

        #region Events

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public JSPlumbInstanceInterop(JSPlumbInterop jsPlumbInterop, ElementReference container, MetadataModel defaults)
        {
            instanceTask = new(async () =>
            {
                var instance = await jsPlumbInterop.Create(container, defaults);

                await jsPlumbInterop.RegisterHandlers(this, instance);

                return instance;
            });
        }
        #endregion

        #region Life Cycle
        public virtual async ValueTask DisposeAsync()
        {
            if (instanceTask.IsValueCreated)
            {
                var instance = await instanceTask.Value;

                await instance.DisposeAsync();
            }
        }
        #endregion

        #region API Methods
        public virtual async ValueTask<IJSObjectReference> Connect(ElementReference source, ElementReference target)
        {
            var instance = await instanceTask.Value;

            return await instance.InvokeAsync<IJSObjectReference>("connect", source, target);
        }
        #endregion

        #region Invokable
        [JSInvokable]
        public virtual async Task<bool> BeforeDropIntercept(MetadataModel args)
        {
            //  TODO: allow registering of before drop interceptors in a chain of response pattern

            return true;
        }

        [JSInvokable]
        public virtual async Task Connected(MetadataModel args)
        {
            //  TODO: Fire connected event so anyone can handle it for the given instance
        }
        #endregion

        #region Helpers
        #endregion
    }
}