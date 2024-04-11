using Application.Abstractions.Messaging;
using Application.Users.CreateUser;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;
using Mapster;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Application.Users.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        => (_unitOfWork, _userRepository) = (ThrowIfNull(unitOfWork), ThrowIfNull(userRepository));

    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var createUserCommandHandler = new CreateUserCommandHandler(_unitOfWork, _userRepository);
        var createUserCommand = request.Adapt<CreateUserCommand>()
                    with        { RolesId = new() { Role.Customer.Id } };

        return await createUserCommandHandler.Handle(createUserCommand, cancellationToken);
    }
}