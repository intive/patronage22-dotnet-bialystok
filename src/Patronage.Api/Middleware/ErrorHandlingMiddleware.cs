using FluentValidation;
using FluentValidation.Results;
using Patronage.Api.Exceptions;
using Patronage.DataAccess;
using System.Text.Json;

namespace Patronage.Api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException validationException)
            {
                var logId = CreateLogWithId(validationException);

                var response = new BaseResponse<IEnumerable<ValidationFailure>>
                {
                    ResponseCode = StatusCodes.Status422UnprocessableEntity,
                    Message = $"One or more validation errors has occured. Error cede: {logId}",
                    BaseResponseError = validationException.Errors
                        .Select(x => new BaseResponseError(x.PropertyName, x.ErrorCode, x.ErrorMessage)).ToList()
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (NotFoundException notFoundException)
            {
                var logId = CreateLogWithId(notFoundException);

                var response = new BaseResponse<IEnumerable<string>>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $" Error cede: {logId}"
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception e)
            {
                var logId = CreateLogWithId(e);

                var response = new BaseResponse<string>
                {
                    ResponseCode = StatusCodes.Status500InternalServerError,
                    Message = $"Some server error has occured. To get to know the details, turn to administrator giving him code: {logId}"
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }

        private string CreateLogWithId(Exception e)
        {
            var logId = Guid.NewGuid().ToString();
            var logContent = "LogId: " + logId + Environment.NewLine +
                             " Source: " + e.Source + Environment.NewLine +
                             " Message: " + e.Message + Environment.NewLine +
                             " InnerException: " + e.InnerException + Environment.NewLine +
                             " Data: " + e.Data + Environment.NewLine +
                             " StackTrace: " + e.StackTrace;

            _logger.LogError(logContent);

            return logId;
        }
    }
}