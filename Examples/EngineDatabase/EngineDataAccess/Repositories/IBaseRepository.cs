namespace EngineDataAccess.Repositories
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(int id);
        T? GetById(int id);
        Task SaveAsync();
        bool HasChanges();
        void Add(T model);
        void Remove(T model);
    }
}
