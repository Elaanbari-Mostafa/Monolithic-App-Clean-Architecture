namespace Application.Users.DeleteUserById;

public sealed class DeleteUserByIdCommandHandler : ICommandHandler<DeleteUserByIdCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserByIdCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
       => (_unitOfWork, _userRepository) = (ThrowIfNull(unitOfWork), ThrowIfNull(userRepository));


    public async Task<Result> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return Result.Failure(DomainErrors.User.UserWithIdNotFound(request.Id));
        }

        _userRepository.DeleteUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}