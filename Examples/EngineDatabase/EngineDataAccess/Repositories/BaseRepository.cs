using Microsoft.EntityFrameworkCore;

namespace EngineDataAccess.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
    {
        protected readonly TContext Context;

        protected BaseRepository(TContext context)
        {
            this.Context = context;
        }
        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model);
        }

        public virtual TEntity? GetById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }
    }
}
