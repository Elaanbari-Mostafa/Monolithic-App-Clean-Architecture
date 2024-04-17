namespace Infrastructure.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasConversion(u => u.Value, u => FirstName.Create(u).Value)
            .HasMaxLength(FirstName.MaxLength);

        builder.Property(u => u.Password)
            .HasConversion(u => u.Value, u => Password.FromHashedString(u));

        builder.Property(u => u.LastName)
            .HasConversion(u => u.Value, u => LastName.Create(u).Value)
             .HasMaxLength(LastName.MaxLength);

        builder.Property(u => u.Email)
            .HasConversion(u => u.Value, u => Email.Create(u).Value)
            .HasMaxLength(Email.MaxLength);

        builder.Property(u => u.DateOfBirth)
            .HasColumnType(DbDataTypes.Date);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasMany(x => x.RoleUsers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}