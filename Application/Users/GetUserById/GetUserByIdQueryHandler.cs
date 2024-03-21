using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Mapster;
using static CustomArgumentNullException;

namespace Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository; 

    public GetUserByIdQueryHandler(IUserRepository userRepository)
        => _userRepository = ThrowIfNull(userRepository);

    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    => Result.Create(await _userRepository.GetUserByIdAsync(request.Id, cancellationToken), DomainErrors.User.UserNotFound(request.Id))
            .Map(u => u.Adapt<GetUserByIdResponse>());
}