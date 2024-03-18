using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(ctr => ctr.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly));
        return service;
    }
}