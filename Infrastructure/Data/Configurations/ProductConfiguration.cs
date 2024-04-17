namespace Infrastructure.Data.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products);

        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.Money,builder =>
        {
            builder.Property(p => p.Currency).HasColumnType(DbDataTypes.Nvarchar(10));
            builder.Property(p => p.Price).HasColumnType(DbDataTypes.Decimal(18,2));
        });
    }
}