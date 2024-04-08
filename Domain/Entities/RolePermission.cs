namespace Domain.Entities;

public sealed class RolePermission
{
    public static IReadOnlyCollection<RolePermission> GetValues()
        => new[]
        {
                Create(Role.Customer,Enums.Permission.SelectUser),
                Create(Role.Customer,Enums.Permission.UpdateUser),                

                Create(Role.Admin,Enums.Permission.CreateUser),
                Create(Role.Admin,Enums.Permission.DeleteUser),
        };

    private static RolePermission Create(Role role, Enums.Permission permission)
        => new()
        {
            PermissionId = (int)permission,
            RoleId = role.Id,
        };

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
}