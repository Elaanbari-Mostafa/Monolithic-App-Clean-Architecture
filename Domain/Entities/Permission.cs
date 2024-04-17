using Domain.Enums;

namespace Domain.Entities;

public sealed class Permission
{
    public static IEnumerable<Permission> GetValues()
        => Enum.GetValues<EPermission>()
               .Select(p => new Permission
               {
                   Id = (int)p,
                   Name = p.ToString()
               });

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}