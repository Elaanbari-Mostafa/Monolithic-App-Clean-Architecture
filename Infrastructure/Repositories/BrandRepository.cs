namespace Infrastructure.Repositories;

public sealed class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
        => _context = ThrowIfNull(context);

    public async Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var brand = await _context.Set<Brand>()
                        .FirstOrDefaultAsync(brand => brand.Id == id, cancellationToken);
        return brand;
    }
}