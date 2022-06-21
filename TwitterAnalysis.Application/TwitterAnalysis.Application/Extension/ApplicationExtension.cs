using Microsoft.Extensions.DependencyInjection;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.App.Services;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;
using TwitterAnalysis.Infrastructure.Service.Gateway;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Context;
using TwitterAnalysis.Infrastructure.Data.Repository;
using TwitterAnalysis.App.Services.ML.Net_Processor;

namespace TwitterAnalysis.Application.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITwitterSearchProcessor, TwitterSearchProcessor>();
            services.AddScoped<IMachineLearningProcessor, MachineLearningProcessor>();
            services.AddScoped<ITwitterSearchQuery, TwitterSearchService>();
            services.AddScoped<ITwitterServiceGateway, TwitterServiceGateway>();

            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<ITweetRepository, TweetRepository>();

            return services;
        }
    }
}
