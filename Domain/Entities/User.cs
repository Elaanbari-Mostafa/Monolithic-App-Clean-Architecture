using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using Domain.ValueObjects;
using System.Data;

namespace Domain.Entities;

public sealed class User : Entity, IAuditableEntity
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public List<RoleUser> RoleUsers { get; private set; }
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
        DateTime dateOfBirth,
        ICollection<Role> roles)
    {
        User newUser = new(Guid.NewGuid(), firstName, lastName, email, password, dateOfBirth);
        if (roles.Any())
        {
            var userRoles = roles.Select(x => new RoleUser() { RoleId = x.Id, UserId = newUser.Id });
            newUser.RoleUsers!.AddRange(userRoles);
        }

        return newUser;
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