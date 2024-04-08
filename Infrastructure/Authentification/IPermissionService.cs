namespace Infrastructure.Authentification;

internal interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsFromUserIdAsync(Guid userId);
}
