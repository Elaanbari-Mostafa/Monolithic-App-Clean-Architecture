using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class User : Entity, IAuditableEntity
{
    public ICollection<Role> Roles { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public ICollection<RoleUser> RoleUsers { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        DateTime dateOfBirth) : base(id)
            => (FirstName, LastName, Email, Password, DateOfBirth, RoleUsers)
             = (firstName, lastName, email, password, dateOfBirth, new List<RoleUser>());

    public static User Create(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        DateTime dateOfBirth)
            => new(Guid.NewGuid(), firstName, lastName, email, password, dateOfBirth);

    public User WithRole(ICollection<Role> Roles)
    {
        foreach (var role in Roles)
        {
            RoleUsers!.Add(new RoleUser() { RoleId = role.Id, UserId = Id});
        }

        return this;
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
        {
            // Raise Domain Event
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public void Update(FirstName firstName, LastName lastName, DateTime dateOfBirth)
        => (FirstName, LastName, DateOfBirth) = (firstName, lastName, dateOfBirth);

    public void AddRoles(HashSet<Role> roles)
    {
        if (roles is null)
        {
            throw new ArgumentNullException(nameof(roles));
        }
        ((List<Role>)Roles).AddRange(roles);
    }

    public Result<Password> ChangePassword(string newPassword)
    {
        var passwordResult = Password.Create(newPassword);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<Password>(passwordResult.Error);
        }

        return Result.Create(passwordResult.Value)
                .Ensure(p => p.VerifyPassword(newPassword), DomainErrors.ValueObject.Password.NewPasswordEqualToThePreviousPassword)
                .OnSuccess(p => Password = p);
    }
}