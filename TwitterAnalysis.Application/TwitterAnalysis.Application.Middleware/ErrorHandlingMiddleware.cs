using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TwitterAnalysis.App.Service.Common;
using TwitterAnalysis.App.Service.Common.Extension;

namespace TwitterAnalysis.Application.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionMessageAsync(context);
            }
        }

        public static Task HandleExceptionMessageAsync(HttpContext context)
        {

            string response = JsonSerializer.Serialize(new ValidationProblemDetails()
            {
                Title = OperationMessageStatusEnum.ErrorFounded.GetDescription(),
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(response);
        }
    }
}
