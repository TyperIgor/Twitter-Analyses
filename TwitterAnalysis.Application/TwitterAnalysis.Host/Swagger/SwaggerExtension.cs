using Microsoft.OpenApi.Models;

namespace TwitterAnalysis.Application.Swagger
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Twitter Analysis API ",
                    Version = "v1",
                    Description = "Api that provides classified racists tweets",
                    Contact = new OpenApiContact()
                    {
                        Name = "Igor Lima",
                        Url = new Uri("https://github.com/TyperIgor/TwitterRacialApplication"),
                    }
                });
            });

            return services;
        }
    }
}
