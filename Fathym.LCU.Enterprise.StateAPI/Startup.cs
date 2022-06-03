using LCU.Hosting.Options;
using LCU.Personas.Enterprises.Architect.EnterprisesAsCode;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Fathym.LCU.Enterprise.StateAPI.Startup))]

namespace Fathym.LCU.Enterprise.StateAPI
{
    
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

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
                        nameof(IEnterprisesAsCodeService),
                        new LCUClientOptions()
                        {
                            BaseAddress = Environment.GetEnvironmentVariable($"{typeof(IEnterprisesAsCodeService).FullName}.BaseAddress")
                        }
                    }
                }
            };

            var registry = builder.Services.AddLCUPollyRegistry(httpOpts);

            builder.Services.AddLCUHTTPClient<IEnterprisesAsCodeService>(registry, httpOpts);
        }
    }
}