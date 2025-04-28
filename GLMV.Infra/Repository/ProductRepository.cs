using GLMV.Domain.Models;
using GLMV.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace GLMV.Infra.Repository
{
    public class ProductRepository : Repository<Product>
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await _appDbContext.Products
                .Include(p => p.Category)
                .Include(s => s.SalesPerson)
                .ToListAsync();
        }

        public async Task<Product?> GetProductWithCategoryByIdAsync(int id)
        {
            return await _appDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllProductsBySalesPerson(string id)
        {
            var products = await _appDbContext.Products.Where(s => s.SalesPersonId == id).ToListAsync();

            return products;
        }

        public bool IsProductExistsAsync(int id)
        {
            return _appDbContext.Products.Any(p => p.Id == id);
        }
    }
}
