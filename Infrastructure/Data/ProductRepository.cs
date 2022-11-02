using Product = Core.Entites.Product;

namespace Infrastructure.Data;
public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext storeContext)
    {
        _context = storeContext;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _context.Products
           .Include(p => p.ProductBrand)
           .Include(p => p.ProductType)
           .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products
        .Include(p => p.ProductBrand)
        .Include(p => p.ProductType)
        .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }
}
