using GLMV.Domain.Interfaces;
using GLMV.Infra.Data;
using Microsoft.EntityFrameworkCore;


namespace GLMV.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(T reg)
        {
            await _appDbContext.Set<T>().AddAsync(reg);
        }

        public Task DeleteAsync(T reg)
        {
            _appDbContext.Set<T>().Remove(reg);

            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var list = await _appDbContext.Set<T>().ToListAsync();

            return list;
        }

        public  T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveASync()
        {
            await _appDbContext.SaveChangesAsync();
        }
        public void Update(T reg)
        {
            _appDbContext.Set<T>().Update(reg);
        }
    }
}