namespace Application.Users.CreateUser;

public sealed record CreateUserCommand(
                        string FirstName,
                        string LastName,
                        string Email,
                        string Password,
                        HashSet<int> RolesId,
                        DateTime DateOfBirth) : ICommand<Guid>;