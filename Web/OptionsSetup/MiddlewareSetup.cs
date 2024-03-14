using Infrastructure.Middlewares;

namespace Web.OptionsSetup;

public static class MiddlewareSetup
{
    public static IApplicationBuilder UseCustomMiddlewareSetup(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}

