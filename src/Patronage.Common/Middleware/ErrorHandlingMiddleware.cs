using FluentValidation;
using Microsoft.AspNetCore.Http;
using Patronage.Common.Exceptions;
using System.Text.Json;

namespace Patronage.Common.Middleware
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
                var response = new
                {
                    title = "One or more validation error has occured.",
                    status = StatusCodes.Status422UnprocessableEntity,
                    detail = validationException.Message,
                    //errors = GetErrors(exception)
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
