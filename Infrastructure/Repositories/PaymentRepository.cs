
namespace Infrastructure.Repositories;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PaymentRepository(ApplicationDbContext dbContext) => _dbContext = ThrowIfNull(dbContext);

    public void AddPayment(Payment payment)
    {
        _dbContext.Add(payment);
    }

    public async Task<Payment?> GetPaymentById(Guid id, CancellationToken cancellationToken)
    {
        Payment? payment = await _dbContext.Set<Payment>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        return payment;
    }

    public void UpdatePayment(Payment payment)
    {
        _dbContext.Update(payment);
    }
}
