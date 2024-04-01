using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions builder) : base(builder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);
}