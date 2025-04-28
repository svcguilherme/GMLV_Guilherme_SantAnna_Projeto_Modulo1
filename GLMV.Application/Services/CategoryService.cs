using GLMV.Domain.Models;
using GLMV.Infra.Repository;


namespace GLMV.Application.Services
{
    public class CategoryService : RepositoryService<Category>
    {
        private readonly CategoryRepository _repository;
        public CategoryService(CategoryRepository categoryRepository) : base(categoryRepository)
        {
            _repository = categoryRepository;
        }

        public bool isCategoryContainProducts(int id)
        {
            return _repository.isCategoryContainProducts(id);
        }

        public bool isCategoryExists()
        {
            return _repository.isCategoryExists();
        }
    }
}
