using Microsoft.AspNetCore.Http;
using static Domain.Exceptions.CustomArgumentNullException;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Infrastructure.Middlewares;

public sealed class CustomUnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public CustomUnauthorizedMiddleware(RequestDelegate next) => _next = ThrowIfNull(next);

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        int statutCode = context.Response.StatusCode;
        if (statutCode == Status401Unauthorized ||
            statutCode == Status403Forbidden)
        {
            // Customize the response for unauthorized requests
            context.Response.ContentType = "application/json";
            var response = new { 
                status = statutCode, 
                message = "Unauthorized"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}