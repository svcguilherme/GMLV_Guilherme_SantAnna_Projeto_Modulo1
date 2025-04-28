using GLMV.Domain.Models;
using GLMV.Infra.Data;


namespace GLMV.Infra.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool isCategoryExists()
        {
            return _appDbContext.Categories.Any() ? true : false;
        }

        public bool isCategoryContainProducts(int id)
        {
            return _appDbContext.Products.Where(p => p.CategoryId == id).Any() ? true : false;
        }
    }
}
