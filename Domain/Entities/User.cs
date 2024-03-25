using Domain.Enums;
using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class User : Entity, IAuditableEntity
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserType UserType { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private User(Guid id, FirstName firstName, LastName lastName, Email email, Password password, UserType userType, DateTime dateOfBirth) : base(id)
        => (FirstName, LastName, Email, Password, UserType, DateOfBirth) = (firstName, lastName, email, password, userType, dateOfBirth);

    public static User Create(FirstName firstName, LastName lastName, Email email, Password password, UserType userType, DateTime dateOfBirth)
        => new(Guid.NewGuid(), firstName, lastName, email, password, userType, dateOfBirth);

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
        {
            // Raise Domain Event
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public void Update(FirstName firstName, LastName lastName, Email email, UserType userType, DateTime dateOfBirth)
        => (FirstName, LastName, Email, UserType, DateOfBirth) = (firstName, lastName, email, userType, dateOfBirth);


    public Result<Password> ChangePassword(Password newPassword)
        => Result.Create(newPassword)
            .Ensure(p => p.Equals(Password), DomainErrors.ValueObject.Password.NewPasswordEqualToThePreviousPassword)
            .OnSuccess(p => Password = p);
}