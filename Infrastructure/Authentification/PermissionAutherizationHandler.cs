using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Authentification;

internal sealed class PermissionAutherizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAutherizationHandler(IServiceScopeFactory serviceScopeFactory)
        => _serviceScopeFactory = serviceScopeFactory;
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(x => x.Properties.Values.Contains(JwtRegisteredClaimNames.Sub))?.Value;
        if (!Guid.TryParse(userId,out Guid parsedUserId))
        {
            return;
        }

        IServiceScope scope = _serviceScopeFactory.CreateScope();
        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        HashSet<string> permission = await permissionService.GetPermissionsFromUserIdAsync(parsedUserId);

        if (permission.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}