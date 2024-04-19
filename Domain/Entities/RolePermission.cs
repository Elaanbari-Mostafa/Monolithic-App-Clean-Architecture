using Domain.Enums;

namespace Domain.Entities;

public sealed class RolePermission
{
    public static IReadOnlyCollection<RolePermission> GetValues()
    {
        return new List<IList<RolePermission>>()
        {
              Create(Role.Customer,
                          EPermission.SelectUser,
                          EPermission.UpdateUser,
                          EPermission.CreateOrder),

              Create(Role.Admin,
                          EPermission.CreateUser,
                          EPermission.DeleteUser,
                          EPermission.CreateOrder),
        }
        .Select(p => p)
        .SelectMany(p => p)
        .ToList();
    }

    private static IList<RolePermission> Create(Role role, params EPermission[] permission)
        => permission.Select(p => new RolePermission()
        {
            PermissionId = (int)p,
            RoleId = role.Id,
        }).ToList();

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
}