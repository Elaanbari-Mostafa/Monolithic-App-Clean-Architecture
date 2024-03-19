using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.User);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasConversion(u => u.Value, u => FirstName.Create(u).Value)
            .HasMaxLength(FirstName.MaxLength);

        builder.Property(u => u.Password)
            .HasConversion(u => u.Value, u => Password.Create(u).Value)
            .HasMaxLength(Password.MaxLength);

        builder.Property(u => u.LastName)
            .HasConversion(u => u.Value, u => LastName.Create(u).Value)
             .HasMaxLength(LastName.MaxLength);

        builder.Property(u => u.Email)
            .HasConversion(u => u.Value, u => Email.Create(u).Value)
            .HasMaxLength(Email.MaxLength);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
