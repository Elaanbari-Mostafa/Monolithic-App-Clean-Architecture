using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Application.Users.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        => (_unitOfWork, _userRepository) = (ThrowIfNull(unitOfWork), ThrowIfNull(userRepository));

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = Verify(
            request,
            out Email? email,
            out FirstName? firstName,
            out LastName? lastName,
            out Password? password);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        if (await _userRepository.IsEmailUniqueAsync(email!, cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse);
        }

        User user = User.Create(firstName!, lastName!, email!, password!, request.UserType, request.DateOfBirth);

        _userRepository.AddUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public static Result Verify(CreateUserCommand request, out Email? email, out FirstName? firstName, out LastName? lastName, out Password? password)
    {
        email = null; firstName = null; lastName = null; password = null;

        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }
        email = emailResult.Value;

        var firstNameResult = FirstName.Create(request.FirstName);
        if (firstNameResult.IsFailure)
        {
            return Result.Failure(firstNameResult.Error);
        }
        firstName = firstNameResult.Value;

        var lastNameResult = LastName.Create(request.LastName);
        if (lastNameResult.IsFailure)
        {
            return Result.Failure(lastNameResult.Error);
        }
        lastName = lastNameResult.Value;

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }
        password = passwordResult.Value;

        return Result.Success();
    }
}