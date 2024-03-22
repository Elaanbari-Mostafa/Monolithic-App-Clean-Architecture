using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    public Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
