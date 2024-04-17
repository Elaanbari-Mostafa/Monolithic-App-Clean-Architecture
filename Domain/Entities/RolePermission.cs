namespace Domain.Entities;

public sealed class RolePermission
{
    public static IReadOnlyCollection<RolePermission> GetValues()
        => new[]
        {
                Create(Role.Customer,Enums.EPermission.SelectUser),
                Create(Role.Customer,Enums.EPermission.UpdateUser),                

                Create(Role.Admin,Enums.EPermission.CreateUser),
                Create(Role.Admin,Enums.EPermission.DeleteUser),
        };

    private static RolePermission Create(Role role, Enums.EPermission permission)
        => new()
        {
            PermissionId = (int)permission,
            RoleId = role.Id,
        };

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
}