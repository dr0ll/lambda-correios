using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class ErrorHandlingMiddleware
    {
        RequestDelegate Next { get; set; }
        ILogger Logger { get; set; }

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.Next = next;
            this.Logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, Logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            logger.LogError($"{exception.StackTrace} {exception.StackTrace}", exception);
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = exception.Message;

            if (exception is NotFoundException)
                code = HttpStatusCode.NotFound;

            else if (exception is UnAuthorized)
                code = HttpStatusCode.Unauthorized;
            else if (exception is BadParametersException)
                code = HttpStatusCode.BadRequest;

            else if (exception is ConflictException)
                code = HttpStatusCode.Conflict;


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
