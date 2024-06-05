using Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        TypeAdapterConfig.GlobalSettings.Scan(ApplicationAssemblyReference.Assembly); // Add Mapster configuration
        service.AddMediatR(ctr => ctr.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly)); // Add MediatR
        service.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly, includeInternalTypes: true); // Add Fluent Validation 
        service.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPiplineBehavior<,>));

        return service;
    }
}