using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

internal interface ICommandHandler<TRequest> : IRequestHandler<TRequest, Result>
    where TRequest : ICommand
{
}

internal interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : ICommand<TResponse>
{
}
