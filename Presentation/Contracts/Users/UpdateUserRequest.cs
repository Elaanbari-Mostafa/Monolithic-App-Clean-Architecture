namespace Presentation.Contracts.Users;

public sealed record UpdateUserRequest(
                    Guid Id,
                    string FirstName,
                    string LastName,
                    string Email,
                    DateTime DateOfBirth);