using Blazorise;
using Fathym.LCU.Utils;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;

namespace Fathym.LCU.CodeBuilder.WASM
{
    public class AppState
    {
        public virtual JsonObject? AppSettings { get; set; }
    }

    public class AppBase : ComponentBase
    {
        #region Inject
        [Inject]
        protected virtual ConfigUtilsJsInterop? configUtils { get; set; }
        #endregion

        #region Fields
        protected readonly AppState appState;

        protected readonly Theme theme;
        #endregion

        #region Constructors
        public AppBase()
        {
            appState = new AppState();

            theme = new Theme
            {
                ColorOptions = new ThemeColorOptions
                {
                    Primary = "#4a918e",
                    Secondary = "#b9dddd"
                },
                TextColorOptions = new ThemeTextColorOptions()
                {
                    Primary = "#4a918e",
                    Secondary = "#b9dddd"
                }
            };
        }
        #endregion

        #region Life Cycle
        protected override async Task OnInitializedAsync()
        {
            appState.AppSettings = await configUtils!.LoadLCUSettings();

            await base.OnInitializedAsync();
        }
        #endregion

        #region Helpers

        #endregion
    }
}