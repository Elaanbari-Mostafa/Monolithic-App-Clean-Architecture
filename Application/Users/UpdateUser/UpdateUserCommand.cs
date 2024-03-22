using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
                        string FirstName,
                        string LastName,
                        string Email,
                        UserType UserType,
                        DateTime DateOfBirth) : ICommand;