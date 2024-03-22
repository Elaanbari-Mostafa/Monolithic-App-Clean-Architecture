using Domain.Errors;
using Microsoft.AspNetCore.Http;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Middlewares
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
            => _next = ThrowIfNull(next);

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(DomainErrors.GlobalErrors.MiddlewareErrorHandler(ex.Message));
            }
        }
    }
}
