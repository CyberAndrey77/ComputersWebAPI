using Computers.Models.Dto;
using System.Net;

namespace Computers.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ArgumentNullException ex)
            {
                await HandleExeptionAsync(context, ex.Message, HttpStatusCode.NotFound, "Not found");
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(context, ex.Message, HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        private async Task HandleExeptionAsync(HttpContext context, string exMessage, HttpStatusCode statusCode, string errorMessage)
        {
            _logger.LogError(exMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            await response.WriteAsJsonAsync(new ErrorDto(errorMessage, statusCode));
        }
    }
}
