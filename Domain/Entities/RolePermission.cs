namespace Domain.Entities;

public sealed class RolePermission
{
    public static IReadOnlyCollection<RolePermission> GetValues()
    {
        return new List<IList<RolePermission>>()
        {
              Create(Role.Customer,
                          Enums.Permission.SelectUser,
                          Enums.Permission.UpdateUser,
                          Enums.Permission.CreateOrder),

              Create(Role.Admin,
                          Enums.Permission.CreateUser,
                          Enums.Permission.DeleteUser,
                          Enums.Permission.CreateOrder),
        }
        .Select(p => p)
        .SelectMany(p => p)
        .ToList();
    }

    private static IList<RolePermission> Create(Role role, params Enums.Permission[] permission)
        => permission.Select(p => new RolePermission()
        {
            PermissionId = (int)p,
            RoleId = role.Id,
        }).ToList();

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
}