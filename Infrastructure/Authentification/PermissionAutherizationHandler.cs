namespace Infrastructure.Authentification;

internal sealed class PermissionAutherizationHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionAutherizationHandler()
    {

    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        HashSet<string> permission = context.User.Claims
                            .Where(x => x.Type == CustomClaims.Permissions)
                            .Select(x => x.Value)
                            .ToHashSet();

        if (permission.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}