using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TwitterAnalysis.App.Service.Model.Settings;
using TwitterAnalysis.App.Services;
using TwitterAnalysis.App.Services.Data_Processor;
using TwitterAnalysis.App.Services.FileProcessor;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.App.Services.ML.Net_Processor;
using TwitterAnalysis.App.Services.AuthLoginProcessor;
using TwitterAnalysis.Application.Auth.Interfaces;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.Application.Validations;
using TwitterAnalysis.Infrastructure.Auth.Jwt;
using TwitterAnalysis.Infrastructure.Data.Context;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Repository;
using TwitterAnalysis.Infrastructure.Service.Gateway;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHostDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region User Layer
            services.AddControllers()
                    .AddFluentValidation(config => config
                    .RegisterValidatorsFromAssemblyContaining<QueryFieldValidator>());
            services.AddControllers()
                    .AddFluentValidation(config => config
                    .RegisterValidatorsFromAssemblyContaining<PhraseRequestValidator>());
            services.AddControllers()
                     .AddFluentValidation(config => config
                     .RegisterValidatorsFromAssemblyContaining<AuthValidator>());
            #endregion

            #region Configuration
            services.Configure<TwitterSettings>(configuration.GetSection(nameof(TwitterSettings)));
            services.Configure<GoogleCredentialsSettings>(configuration.GetSection(nameof(GoogleCredentialsSettings)));
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            #endregion

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSettings:Key").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Application/Services Layers 
            services.AddScoped<ITwitterSearchProcessor, TwitterSearchProcessor>();
            services.AddScoped<ITrainingDataProcessor, TrainingDataProcessor>();
            services.AddScoped<IMachineLearningProcessor, MachineLearningProcessor>();
            services.AddScoped<ITwitterSearchQuery, TwitterSearchService>();
            services.AddScoped<IGoogleSheetsApiProcessor, GoogleSheetsProcessor>();
            services.AddScoped<ITrainingDataProcessor, TrainingDataProcessor>();
            services.AddScoped<ITrainingAlgorithmProcessor, TrainingAlgorithmProcessor>();
            services.AddScoped<ILoginAuthProcessor, LoginAuthProcessor>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthLoginService, AuthLoginProcessor>();
            #endregion

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            #region Infrastructure Layers
            services.AddScoped<ITwitterServiceGateway, TwitterServiceGateway>();
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<IDataTrainingRepository, DataTrainingRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            #endregion

            return services;
        }
    }
}
