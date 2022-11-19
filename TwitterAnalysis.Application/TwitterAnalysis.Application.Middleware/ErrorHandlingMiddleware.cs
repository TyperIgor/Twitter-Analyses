using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TwitterAnalysis.App.Service.Common;
using TwitterAnalysis.App.Service.Common.Extension;

namespace TwitterAnalysis.Application.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch
            {
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
