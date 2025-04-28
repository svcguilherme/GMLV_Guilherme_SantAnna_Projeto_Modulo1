namespace GLMV.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T reg);
        void Update(T reg);
        Task DeleteAsync(T reg);
        T GetById(int id);
        Task<List<T>> GetAllAsync();
        Task SaveASync();

    }
}
