namespace Infrastructure.Data.Configurations;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(TableNames.Payments);

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Order)
            .WithOne()
            .HasForeignKey<Payment>(p => p.OrderId);

        builder.Property(p => p.Status)
           .HasConversion(s => s.ToString(), s => Enum.Parse<PaymentStatus>(s));

        builder.Property(p => p.Method)
           .HasConversion(s => s.ToString(), s => Enum.Parse<PaymentMethod>(s));

        builder.OwnsOne(p => p.Amount);
    }
}
