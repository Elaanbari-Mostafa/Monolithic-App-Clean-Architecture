using Application.Abstractions.Messaging;

namespace Application.Users.DeleteUserById;

public sealed record DeleteUserByIdCommand(Guid Id) : ICommand;