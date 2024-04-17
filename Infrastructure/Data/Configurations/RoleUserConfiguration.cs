namespace Infrastructure.Data.Configurations;

internal sealed class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.ToTable(TableNames.RoleUser);
    }
}