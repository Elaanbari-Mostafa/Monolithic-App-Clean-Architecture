namespace Infrastructure.Data.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermission);

        builder.HasKey(p => new { p.RoleId, p.PermissionId });
        
        builder.HasData(RolePermission.GetValues());
    }
}