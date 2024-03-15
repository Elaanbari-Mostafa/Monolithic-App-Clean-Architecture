using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class GlobalErrors
    { 
        public static Func<string, Error> MiddlewareErrorHandler => error => new("GlobalErrors.MiddlewareErrorHandler", error);
    }
}