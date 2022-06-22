using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Security.Exceptions;
using GymMGMT.Domain.Exceptions;
using Newtonsoft.Json;

namespace GymMGMT.Api.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            } 
            catch(Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                UnprocessableEntityException => StatusCodes.Status422UnprocessableEntity,
                AppValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                AppException applicationException => applicationException.Title,
                _ => "Server Error"
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is AppValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }
    }
}