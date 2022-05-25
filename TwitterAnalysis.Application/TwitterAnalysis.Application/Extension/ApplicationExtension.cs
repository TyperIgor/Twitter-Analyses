using Microsoft.Extensions.DependencyInjection;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.App.Services;

namespace TwitterAnalysis.Application.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITwitterSearchProcessor, TwitterSearchProcessor>();
            services.AddScoped<ITwitterSearchQuery, TwitterSearchService>();

            return services;
        }
    }
}
