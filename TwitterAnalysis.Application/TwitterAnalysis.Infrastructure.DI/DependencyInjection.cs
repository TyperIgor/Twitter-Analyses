using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterAnalysis.App.Services;
using TwitterAnalysis.App.Services.FileProcessor;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.App.Services.ML.Net_Processor;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.Application.Validations;
using TwitterAnalysis.Infrastructure.Data.Context;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Repository;
using TwitterAnalysis.Infrastructure.Service.Gateway;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITwitterSearchProcessor, TwitterSearchProcessor>();
            services.AddScoped<IMachineLearningProcessor, MachineLearningProcessor>();
            services.AddScoped<ITwitterSearchQuery, TwitterSearchService>();

            services.AddScoped<ITwitterServiceGateway, TwitterServiceGateway>();
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<ITweetRepository, TweetRepository>();

            services.AddControllers()
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<QueryFieldValidator>());

            services.AddScoped<IGoogleSheetsApiProcessor, GoogleSheetsProcessor>();

            return services;
        }
    }
}
