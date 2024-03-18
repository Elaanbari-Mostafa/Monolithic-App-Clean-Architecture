using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

internal interface IQuery<TValue> : IRequest<Result<TValue>>
{
}
