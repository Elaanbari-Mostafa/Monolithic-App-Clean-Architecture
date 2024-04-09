namespace Domain.Entities;

public sealed class RoleUser
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}