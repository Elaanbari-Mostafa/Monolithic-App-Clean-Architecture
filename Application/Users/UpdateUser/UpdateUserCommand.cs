namespace Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
                        Guid Id,
                        string FirstName,
                        string LastName,
                        DateTime DateOfBirth) : ICommand;