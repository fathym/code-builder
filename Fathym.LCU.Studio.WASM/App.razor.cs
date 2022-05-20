using Blazorise;
using Fathym.LCU.IDE.Controls;
using Fathym.LCU.Studio.WASM.State;
using Fathym.LCU.Utils;
using Microsoft.AspNetCore.Components;

namespace Fathym.LCU.Studio.WASM
{
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
                BackgroundOptions = new ThemeBackgroundOptions()
                {
                    Primary = "#4a918e",
                    Secondary = "#b9dddd"
                },
                BarOptions = new ThemeBarOptions()
                {
                    DarkColors = new ThemeBarColorOptions()
                    {
                        DropdownColorOptions = new ThemeBarDropdownColorOptions()
                        {
                            BackgroundColor = "#3b6460"
                        }
                    },
                    LightColors = new ThemeBarColorOptions()
                    {
                        DropdownColorOptions = new ThemeBarDropdownColorOptions()
                        {
                            BackgroundColor = "#8fc8c8"
                        }
                    }
                },
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

            appState.Studio = new LCUStudioState()
            {
                CurrentPackage = "SelectedPackage",
                CurrentPlugin = "SelectedPlugin",
                Header = new LCUStudioHeaderState()
                {
                    Title = "MyStudio",
                    Items = new List<IDEBarItemState>()
                    {
                        new IDEBarItemState()
                        {
                            Text = "Getting Started",
                            Position = IDEBarItemPositionTypes.Start,
                            Items = new List<IDEBarItemState>()
                            {
                                new IDEBarItemState()
                                {
                                    Text = "Walkthrough of Studio Features"
                                },
                                new IDEBarItemState(),
                                new IDEBarItemState()
                                {
                                    Text = "Coming Soon"
                                }
                            }
                        },
                        new IDEBarItemState()
                        {
                            Text = "Documentation",
                            Path = "https://www.fathym.com/docs"
                        },
                        new IDEBarItemState()
                        {
                            Text = "Hey",
                            Path = "./there",
                            Position = IDEBarItemPositionTypes.End
                        }
                    }
                },
                ActivityBar = new LCUStudioActivityBarState()
                {
                    Title = "Fathym Studio",
                    Icon = "https://www.fathym.com/assets/images/logo.png",
                    Items = new List<IDEBarItemState>()
                    {
                        new IDEBarItemState()
                        {
                            Text = "Plugin 1",
                            Icon = "fa-phone",
                            Path = "/studio/package-1/plugin-1"
                        },
                        new IDEBarItemState()
                        {
                            Text = "TextGrid",
                            Icon = "fa-mobile-screen-button",
                            Path = "/studio/text-grid/apis"
                        },
                        new IDEBarItemState()
                        {
                            Text = "MyStudio",
                            Icon = "fa-paintbrush",
                            Path = "/mystudio",
                            Position = IDEBarItemPositionTypes.End
                        },
                        new IDEBarItemState()
                        {
                            Text = "Settings",
                            Icon = "fa-gears",
                            Path = "/settings",
                            Position = IDEBarItemPositionTypes.End
                        }
                    }
                }
            };

            await base.OnInitializedAsync();
        }
        #endregion

        #region Helpers

        #endregion
    }
}