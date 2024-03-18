using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

[ApiController]
internal abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;
    protected ApiController(ISender sender)
        => Sender = sender ?? throw new ArgumentNullException(nameof(sender));
}