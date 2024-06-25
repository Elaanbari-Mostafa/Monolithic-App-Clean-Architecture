using Microsoft.AspNetCore.Http;

namespace Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender ?? throw new ArgumentNullException(nameof(sender));

    protected IActionResult HandleFailue(Result result)
        => result switch
        {
            { IsFailure: false } => throw new InvalidOperationException("This result is not failure"),
            IValidationResult validationResult => BadRequest(
                    CreateProblemeDetails("Validation Error",
                                      StatusCodes.Status400BadRequest,
                                      result.Error,
                                      validationResult.Errors)),
            _ => BadRequest(
                    CreateProblemeDetails("Bad request",
                                      StatusCodes.Status400BadRequest,
                                      result.Error)),
        };

    private static ProblemDetails CreateProblemeDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Status = status,
            Type = error.Code,
            Detail = error.Message,
            Extensions = { { nameof(errors), errors } }
        };
}