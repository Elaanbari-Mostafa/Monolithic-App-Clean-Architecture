using Microsoft.AspNetCore.Http;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Middlewares
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) => _next = ThrowIfNull(next);

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
                var response = new
                {
                    status = 500,
                    message = ex.Message,
                    details = ex.InnerException?.Message,
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}