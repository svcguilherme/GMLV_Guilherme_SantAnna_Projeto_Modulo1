using GLMV.Domain.Models;
using GLMV.Infra.Repository;

namespace GLMV.Application.Services
{
    public class SalesPersonService : RepositoryService<SalesPerson>
    {
        private readonly SalesPersonRepository _repository;
        public SalesPersonService(SalesPersonRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public SalesPerson GetByIdString(string id)
        {
            return _repository.GetByIdString(id);
        }

        public bool isSalesPersonExists(string id)
        {
            return _repository.isSalesPersonExists(id);
        }

        public SalesPerson GetSalesPersonProduct(string id)
        {
            return _repository.GetSalesPersonProduct(id);
        }

        public bool isProductBySalesPerson(string id)
        {
            return _repository.isExistProductsBySalesPerson(id);
        }

    }
}
