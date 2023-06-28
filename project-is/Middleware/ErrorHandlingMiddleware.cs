using Microsoft.Extensions.Logging;
using project_is.Exceptions;
using System.Text.Json;

namespace project_is.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            this._logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private async void HandleException(HttpContext httpContext, Exception exception)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";

            switch (exception)
            {

                case BadRequestException e:
                    response.StatusCode = 400;
                    break;
                case LoginCustomException e:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case BuslineCustomException e:
                    response.StatusCode = StatusCodes.Status408RequestTimeout;
                    break;
                default:
                    response.StatusCode = 500;
                    break;
            }
            var message = exception.Message;
            _logger.LogError(message);
            var model = JsonSerializer.Serialize(new { msg = message });
            await response.WriteAsync(model);
        }
       

    }
    public static class ErrorHandlingMiddlewareExtension
    {

        public static void Register(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
