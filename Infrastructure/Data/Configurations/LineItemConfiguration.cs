namespace Infrastructure.Data.Configurations;

internal sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.ToTable(TableNames.LineItems);

        builder.HasKey(x => x.Id);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(l => l.ProductId);

        builder.OwnsOne(l => l.Money, builder =>
        {
            builder.Property(l => l.Currency).HasColumnType(DbDataTypes.Nvarchar(10));
            builder.Property(l => l.Price).HasColumnType(DbDataTypes.Decimal(18, 2));
        });
    }
}