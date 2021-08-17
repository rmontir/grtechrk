using Microsoft.Extensions.DependencyInjection;
using System;

namespace GrTechRK.External.ZenQuotes
{
    public static class IServiceCollectionExtensions
    {
        public static void AddZenquoteServices(this IServiceCollection services, Action<ZenquoteOptions> setupAction)
        {
            services.AddSingleton<IZenquoteService, ZenquoteService>();
            services.AddOptions<ZenquoteOptions>();
            services.Configure(setupAction);
        }
    }
}
