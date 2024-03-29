namespace Infrastructure.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal static class InterceptorsConfigurator
{
    public static DbContextOptionsBuilder AddCustomInterceptors(
         this DbContextOptionsBuilder optionsBuilder, IServiceProvider serviceProvider)
    {
        var auditableInterceptor = serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>()!;
        optionsBuilder.AddInterceptors(auditableInterceptor);

        return optionsBuilder;
    }
}