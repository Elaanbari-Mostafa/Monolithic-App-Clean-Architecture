using Domain.Entities;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable(TableNames.Brands);

        builder.HasKey(x => x.Id);

        builder.HasMany(b => b.Products)
           .WithOne(p => p.Brand)
           .HasForeignKey(p => p.BrandId);
    }
}
