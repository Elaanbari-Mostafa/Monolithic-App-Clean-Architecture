namespace Infrastructure.Authentification;

internal sealed class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
         => _context = ThrowIfNull(context);

    public async Task<HashSet<string>> GetPermissionsFromUserIdAsync(Guid userId)
    {
        var permissions = await _context.Set<User>()
                                .Where(x => x.Id == userId)
                                .Include(x => x.RoleUsers)
                                .ThenInclude(x => x.Role)
                                .ThenInclude(x => x.Permissions)
                                .Select(x => x.RoleUsers
                                                .Select(x => x.Role)
                                                .SelectMany(x => x.Permissions))
                                .SelectMany(x => x.Select(x => x.Name))
                                .ToArrayAsync();

        return permissions.ToHashSet();
    }
}
