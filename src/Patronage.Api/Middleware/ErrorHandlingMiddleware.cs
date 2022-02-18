using FluentValidation;
using FluentValidation.Results;
using Patronage.Api.Exceptions;
using Patronage.DataAccess;
using System.Text.Json;

namespace Patronage.Api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
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
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong.");
            }
        }
    }
}
