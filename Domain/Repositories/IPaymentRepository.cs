using Domain.Entities;

namespace Domain.Repositories;

public interface IPaymentRepository
{
    void AddPayment(Payment payment);
    void UpdatePayment(Payment payment);
    Task<Payment?> GetPaymentById(Guid id, CancellationToken cancellationToken = default);
}
