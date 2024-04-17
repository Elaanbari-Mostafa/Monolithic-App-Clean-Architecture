namespace Presentation.Contracts.Users;

public sealed record CreateUserRequest(
                        string FirstName,
                        string LastName,
                        string Email,
                        string Password,
                        HashSet<int> RolesId,
                        DateTime DateOfBirth);
