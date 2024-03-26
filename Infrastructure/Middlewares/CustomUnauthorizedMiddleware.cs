using Microsoft.AspNetCore.Http;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Middlewares;

public sealed class CustomUnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public CustomUnauthorizedMiddleware(RequestDelegate next) => _next = ThrowIfNull(next);

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            // Customize the response for unauthorized requests
            context.Response.ContentType = "application/json";
            var response = new { 
                status = 401, 
                message = "Unauthorized"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}