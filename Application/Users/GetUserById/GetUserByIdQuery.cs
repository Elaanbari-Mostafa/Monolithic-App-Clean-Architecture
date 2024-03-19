using Application.Abstractions.Messaging;

namespace Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResponse>;