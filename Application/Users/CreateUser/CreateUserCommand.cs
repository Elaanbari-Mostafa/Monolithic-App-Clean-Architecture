using Application.Abstractions.Messaging;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Users.CreateUser;

public sealed record CreateUserCommand(
                        string FirstName,
                        string LastName,
                        string Email,
                        string Password,
                        UserType UserType,
                        DateTime DateOfBirth) : ICommand<Guid>;