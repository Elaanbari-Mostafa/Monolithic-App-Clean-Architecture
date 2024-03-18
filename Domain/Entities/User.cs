using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

internal sealed class User : Entity, IAuditableEntity
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public User(Guid id, FirstName firstName, LastName lastName, Email email, DateTime dateOfBirth) : base(id)
      => (FirstName, LastName, Email, DateOfBirth) = (firstName, lastName, email, dateOfBirth);

}