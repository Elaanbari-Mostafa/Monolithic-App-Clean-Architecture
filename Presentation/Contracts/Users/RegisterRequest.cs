namespace Presentation.Contracts.Users;

public sealed record RegisterRequest(
                        string FirstName,
                        string LastName,
                        string Email,
                        string Password,
                        DateTime DateOfBirth);
