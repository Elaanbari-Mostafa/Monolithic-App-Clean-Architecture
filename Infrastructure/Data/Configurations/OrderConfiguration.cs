namespace Infrastructure.Data.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Orders);

        builder.HasKey(x => x.Id);

        builder.HasOne(o => o.User)
            .WithMany();

        builder.HasMany(o => o.LineItems)
            .WithOne()
            .HasForeignKey(l => l.OrderId);
    }
}
