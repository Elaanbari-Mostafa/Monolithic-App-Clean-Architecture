using Domain.Enums;

namespace Presentation.Contracts.Users;

public sealed record UpdateUserRequest(
                    Guid Id,
                    string FirstName,
                    string LastName,
                    string Email,
                    UserType UserType,
                    DateTime DateOfBirth);

