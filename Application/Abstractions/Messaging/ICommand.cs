using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

internal interface ICommand : IRequest<Result>
{
}

internal interface ICommand<TValue> : IRequest<Result<TValue>>
{
}
