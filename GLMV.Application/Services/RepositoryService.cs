using GLMV.Domain.Interfaces;


namespace GLMV.Application.Services
{
    public class RepositoryService<Entity> : IRepository<Entity>
    {
        private readonly IRepository<Entity> _repository;

        public RepositoryService(IRepository<Entity> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Entity reg)
        {
            await _repository.AddAsync(reg);
        }

        public async Task DeleteAsync(Entity reg)
        {
            await _repository.DeleteAsync(reg);
        }
        public async Task<List<Entity>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync(); ;

            return list;
        }

        public Entity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task SaveASync()
        {
            await _repository.SaveASync();
        }

        public void Update(Entity reg)
        {
            _repository.Update(reg);
        }
    }
}
