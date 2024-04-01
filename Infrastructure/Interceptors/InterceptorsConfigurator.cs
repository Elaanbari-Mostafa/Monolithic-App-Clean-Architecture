using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Interceptors;

internal static class DbInterceptorsConfigurator
{
    internal static DbContextOptionsBuilder AddCustomDbInterceptors(
         this DbContextOptionsBuilder optionsBuilder,
         IServiceProvider serviceProvider)
    {
        var auditableInterceptor = serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>()!;
        optionsBuilder.AddInterceptors(auditableInterceptor);

        return optionsBuilder;
    }
}