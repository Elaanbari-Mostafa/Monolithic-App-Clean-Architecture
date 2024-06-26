﻿using Microsoft.AspNetCore.Http;

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
                    details = new
                    {
                        message = ex.InnerException?.Message,
                        innerException_message = ex.InnerException?.InnerException?.InnerException?.Message,
                        source = ex.InnerException?.Source,

                    }
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}