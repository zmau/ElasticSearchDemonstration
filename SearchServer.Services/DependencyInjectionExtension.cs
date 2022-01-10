using System;
using Microsoft.Extensions.DependencyInjection;
using SearchServer.Services.TextSearcher;

namespace SearchServer.Services
{
    public static class DependencyInjectionExtension
    {
        public static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITextSearcher, TextSearcher.TextSearcher>();
        }
    }
}
