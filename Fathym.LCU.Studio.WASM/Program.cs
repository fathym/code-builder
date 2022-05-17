using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using BlazorPro.BlazorSize;
using Fathym.LCU.Utils;
using Fathym.LCU.Studio.WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorise.Icons.FontAwesome;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ConfigUtilsJsInterop>();

builder.Services.AddMediaQueryService();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddMaterialProviders()
    .AddMaterialIcons()
    .AddFontAwesomeIcons();

await builder.Build().RunAsync();
