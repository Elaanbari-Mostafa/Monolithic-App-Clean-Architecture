using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        => (_userRepository, _unitOfWork) = (ThrowIfNull(userRepository), ThrowIfNull(unitOfWork));

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = Verify(
            request,
            out Email? email,
            out FirstName? firstName,
            out LastName? lastName);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return Result.Failure(DomainErrors.User.UserNotFound(request.Id));
        }

        user.Update(firstName!, lastName!, email!, request.UserType, request.DateOfBirth);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public static Result Verify(UpdateUserCommand request, out Email? email, out FirstName? firstName, out LastName? lastName)
    {
        email = null; firstName = null; lastName = null;

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

        return Result.Success();
    }
}
