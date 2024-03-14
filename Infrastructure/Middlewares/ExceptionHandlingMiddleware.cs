using Domain.Errors;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
            => _next = next ?? throw new ArgumentNullException(nameof(next));

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
