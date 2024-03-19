using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
        => _userRepository = CustomArgumentNullException.ThrowIfNull(userRepository);

    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result.Failure<GetUserByIdResponse>(Error.NullValue);
        }

        var getUserByIdResponse = new GetUserByIdResponse(user.FirstName.Value, user.LastName.Value, user.Email.Value, (int)user.UserType, default, default, default);

        return getUserByIdResponse;
    }
}
