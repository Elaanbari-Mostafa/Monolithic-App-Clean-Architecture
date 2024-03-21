using Application.Abstractions.Messaging;

namespace Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResponse>;