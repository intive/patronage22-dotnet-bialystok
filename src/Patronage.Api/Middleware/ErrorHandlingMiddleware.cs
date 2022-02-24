using FluentValidation;
using FluentValidation.Results;
using Patronage.Api.Exceptions;
using Patronage.DataAccess;
using System.Text.Json;
using System.Linq;

namespace Patronage.Api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILoggerFactory logger;

        public ErrorHandlingMiddleware(ILoggerFactory logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException validationException)
            {
                var response = new BaseResponse<IEnumerable<ValidationFailure>>
                {
                    ResponseCode = StatusCodes.Status422UnprocessableEntity,
                    Message = "One or more validation errors has occured.",
                    BaseResponseError = validationException.Errors
                        .Select(x => new BaseResponseError
                        {
                            PropertyName = x.PropertyName,
                            Code = x.ErrorCode,
                            Message = x.ErrorMessage
                        }).ToList()            
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (NotFoundException notFoundException)
            {
                var response = new BaseResponse<IEnumerable<string>>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = notFoundException.Message
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception e)
            {
                var response = new BaseResponse<IEnumerable<string>>
                {
                    ResponseCode = StatusCodes.Status500InternalServerError,
                    Message = e.Message
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
