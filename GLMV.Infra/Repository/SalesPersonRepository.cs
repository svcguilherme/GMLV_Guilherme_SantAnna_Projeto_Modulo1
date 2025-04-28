using GLMV.Domain.Models;
using GLMV.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace GLMV.Infra.Repository
{
    public class SalesPersonRepository : Repository<SalesPerson>
    {
        private readonly AppDbContext _appDbContext;
        public SalesPersonRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<SalesPerson> GetByIdStringAsync(string id)
        {
            return await _appDbContext.SalesPersons.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public SalesPerson GetByIdString(string id)
        {
            return _appDbContext.SalesPersons.FirstOrDefault(x => x.Id.Equals(id));
        }

        public bool isExistProductsBySalesPerson(string id)
        {
            return _appDbContext.Products.Where(s => s.SalesPersonId == id).Any();
        }

        public bool isSalesPersonExists(string id)
        {
            return _appDbContext.SalesPersons.Any(p => p.Id == id);
        }

        public SalesPerson GetSalesPersonProduct(string id)
        {
            var model = _appDbContext.SalesPersons
                 .Include(s => s.Products).FirstOrDefault(s => s.Id == id);

            return model;
        }
    }
}
