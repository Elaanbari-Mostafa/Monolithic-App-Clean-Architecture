using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<IDbInterceptor> _dbInterceptors;
    private const string _connectionStringName = "DefaultConnection";

    public ApplicationDbContext(
        IConfiguration configuration,
        IEnumerable<IDbInterceptor> dbInterceptors) : base()
         => (_configuration, _dbInterceptors)
          = (ThrowIfNull(configuration), dbInterceptors);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder
                .UseSqlServer(_configuration.GetConnectionString(_connectionStringName))
                .AddInterceptors(_dbInterceptors);
}