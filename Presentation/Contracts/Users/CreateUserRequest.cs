using Domain.Enums;

namespace Presentation.Contracts.Users;

public sealed record CreateUserRequest(
                        string FirstName,
                        string LastName,
                        string Email,
                        string Password,
                        UserType UserType,
                        DateTime DateOfBirth);
