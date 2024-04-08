using Domain.Enums;

namespace Domain.Entities;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Customer = new(1, nameof(Customer));
    public static readonly Role Admin = new(2, nameof(Admin));
    public static readonly Role SuperAdmin = new(3, nameof(SuperAdmin));

    public Role(int id, string name) : base(id, name)
    {
    }

    public ICollection<User> Users { get; set; }
    public ICollection<Permission> Permissions { get; set; }
}