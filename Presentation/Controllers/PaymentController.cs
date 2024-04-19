using Application.Payments.CreatePayment;
using Presentation.Contracts.Payments;

namespace Presentation.Controllers;

public sealed class PaymentController : ApiController
{
    public PaymentController(ISender sender) : base(sender)
    {
    }

    [HttpPost(Routers.Payment.Create)]
    public async Task<IActionResult> CreatePaymentAsync(
        [FromBody] CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        CreatePaymentCommand command = request.Adapt<CreatePaymentCommand>();

        var result = await Sender.Send(command, cancellationToken);

        return result.MapActionResult(
            id => Ok(id),
            BadRequest);
    }
}