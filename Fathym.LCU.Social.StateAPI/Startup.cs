using LCU.Personas.StateAPI.StateServices;
using LCU.Configuration;
using LCU.Graphs;
using LCU.Hosting.Options;
using LCU.Personas.Applications.NPM;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using LCU.Personas.Enterprises.Architect.State;

[assembly: FunctionsStartup(typeof(Fathym.LCU.Social.StateAPI.Startup))]

namespace Fathym.LCU.Social.StateAPI
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddLogging();

            builder.Services.AddHttpContextAccessor();

            builder.Services.RegisterEaCStateService();

            var httpOpts = new LCUStartupHTTPClientOptions()
            {
                CircuitBreakDurationSeconds = 5,
                CircuitFailuresAllowed = 5,
                LongTimeoutSeconds = 60,
                RetryCycles = 3,
                RetrySleepDurationMilliseconds = 500,
                TimeoutSeconds = 30,
                Options = new System.Collections.Generic.Dictionary<string, LCUClientOptions>()
                {
                    {
                        nameof(INPMRegistryService),
                        new LCUClientOptions()
                        {
                            BaseAddress = Environment.GetEnvironmentVariable($"{typeof(INPMRegistryService).FullName}.BaseAddress")
                        }
                    }
                }
            };

            var registry = builder.Services.AddLCUPollyRegistry(httpOpts);
        }
    }
}