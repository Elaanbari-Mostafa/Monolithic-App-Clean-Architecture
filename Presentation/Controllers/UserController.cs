using Application.Users.CreateUser;
using Application.Users.GetUserById;
using Application.Users.UpdateUser;
using Domain.Shared;
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

        return result.MapActionResult(
            onSuccess: value => Ok(value),
            onFailure: BadRequest);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetUserByIdQuery(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(Ok, BadRequest);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateUserCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess 
            ? NoContent() 
            : NotFound(result.Error);
    }
}
