namespace Application.Payments.CreatePayment;

public sealed record  CreatePaymentCommand(Guid OrderId, PaymentMethod Method ) : ICommand<Guid>;