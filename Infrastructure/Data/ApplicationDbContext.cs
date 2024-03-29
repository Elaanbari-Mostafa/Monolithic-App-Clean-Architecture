using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private const string _connectionStringName = "DefaultConnection";

    public ApplicationDbContext(IConfiguration configuration)
        => _configuration = ThrowIfNull(configuration);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
                .UseSqlServer(_configuration.GetConnectionString(_connectionStringName));
}