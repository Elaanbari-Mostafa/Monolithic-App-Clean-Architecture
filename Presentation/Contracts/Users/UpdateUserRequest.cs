namespace Presentation.Contracts.Users;

public sealed record UpdateUserRequest(
                    Guid Id,
                    string FirstName,
                    string LastName,
                    DateTime DateOfBirth);