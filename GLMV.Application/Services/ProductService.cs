using GLMV.Domain.Models;
using GLMV.Infra.Repository;

namespace GLMV.Application.Services
{
    public class ProductService : RepositoryService<Product>
    {
        private readonly ProductRepository _repository;
        public ProductService(ProductRepository productRepository) : base(productRepository)
        {
            _repository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategories()
        {
            return await _repository.GetAllProductsWithCategoriesAsync();
        }

        public async Task<Product> GetAllProductsWithCategoriesByIdAsync(int id)
        {
            return await _repository.GetProductWithCategoryByIdAsync(id);
        }

        public async Task<List<Product>> GetAllProductsBySalesPerson(string id)
        {
            return await _repository.GetAllProductsBySalesPerson(id);
        }

        public bool isProductExistsAsync(int id)
        {
            return _repository.IsProductExistsAsync(id);
        }
    }
}
