namespace Application.Payments.CreatePayment;

internal sealed class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .NotNull()
            .WithMessage("OrderId is required")
            .IsGuid()
            .WithMessage("OrderId must be a valid GUID");

        RuleFor(p => p.Method)
            .IsInEnum()
            .WithMessage("Invalid payment method.");
    }
}