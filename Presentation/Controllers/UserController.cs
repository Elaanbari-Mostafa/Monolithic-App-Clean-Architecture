using Application.Users.CreateUser;
using Application.Users.DeleteUserById;
using Application.Users.GetUserById;
using Application.Users.Login;
using Application.Users.Register;
using Application.Users.UpdateUser;
using Presentation.Contracts.Users;

namespace Presentation.Controllers;

public sealed class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HasPermission(EPermission.CreateUser)]
    [HttpPost(Routers.User.Create)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateUserCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(
            onSuccess: value => Ok(value),
            onFailure: BadRequest);
    }

    [HasPermission(EPermission.SelectUser)]
    [HttpGet(Routers.User.GetUserById)]
    public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetUserByIdQuery(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(Ok, BadRequest);
    }

    [HasPermission(EPermission.UpdateUser)]
    [HttpPut(Routers.User.Update)]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateUserCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(NoContent, NotFound);
    }

    [HttpPost(Routers.User.Login)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<LoginCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(Ok, NotFound);
    }

    [HttpPost(Routers.User.Register)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<RegisterCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(
            onSuccess: value => Ok(value),
            onFailure: BadRequest);
    }

    [HasPermission(EPermission.DeleteUser)]
    [HttpDelete(Routers.User.DeleteById)]
    public async Task<IActionResult> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserByIdCommand(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(Ok, BadRequest);
    }
}