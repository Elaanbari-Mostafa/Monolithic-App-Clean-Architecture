namespace Presentation.Contracts.Payments;

public sealed record class CreatePaymentRequest(Guid OrderId, PaymentMethod Method);
