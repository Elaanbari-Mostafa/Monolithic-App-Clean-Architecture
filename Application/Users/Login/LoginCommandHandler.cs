using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;
using Mapster;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Application.Users.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        => (_userRepository, _jwtProvider) = (ThrowIfNull(userRepository), ThrowIfNull(jwtProvider));

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(emailResult.Error);
        }

        var user = await _userRepository.GetUserByEmailAsync(emailResult.Value, cancellationToken);
        if (user is null)
        {
            return Result.Failure<LoginResponse>(DomainErrors.User.UserWithEmailNotFound(request.Email));
        }

        if (!user.Password.VerifyPassword(request.Password))
        {
            return Result.Failure<LoginResponse>(DomainErrors.User.InvalidCredentials);
        }
       
        var token = await _jwtProvider.Generate(user);
        var loginResponse = new LoginResponse(token);

        return loginResponse;
    }
}