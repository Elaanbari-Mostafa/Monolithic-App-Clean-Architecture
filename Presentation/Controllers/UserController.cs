using Application.Users.CreateUser;
using Application.Users.GetUserById;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Users;

namespace Presentation.Controllers;

[Route("api/users")]
public sealed class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateUserCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Error);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetUserByIdQuery(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}
