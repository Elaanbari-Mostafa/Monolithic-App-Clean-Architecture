using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
                        Guid Id,
                        string FirstName,
                        string LastName,
                        string Email,
                        DateTime DateOfBirth) : ICommand;