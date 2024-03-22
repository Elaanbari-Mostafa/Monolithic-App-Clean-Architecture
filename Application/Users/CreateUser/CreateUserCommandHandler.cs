using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Primitives;
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
        var test = VerifyValueObjects(
            Email.Create(request.Email), 
            FirstName.Create(request.FirstName));

        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        var firstNameResult = FirstName.Create(request.FirstName);
        if (firstNameResult.IsFailure)
        {
            return Result.Failure<Guid>(firstNameResult.Error);
        }

        var lastNameResult = LastName.Create(request.LastName);
        if (lastNameResult.IsFailure)
        {
            return Result.Failure<Guid>(lastNameResult.Error);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<Guid>(passwordResult.Error);
        }

        User user = User.Create(firstNameResult.Value, lastNameResult.Value, emailResult.Value, passwordResult.Value, request.UserType, request.DateOfBirth);

        _userRepository.AddUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }


    public Result<object> VerifyValueObjects(params Result<object>[] props)
    {
        var test = new List<Result<object>>();
        foreach (var item in props)
        {
            if (item.IsFailure)
            {
                return Result.Failure<object>(item.Error);
            }
            test.Add(item);
        }

        return Result.Success<object>(test);
    }
}