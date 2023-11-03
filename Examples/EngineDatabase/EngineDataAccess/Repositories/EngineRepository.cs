using EngineModels.EngineModels;
using Microsoft.EntityFrameworkCore;

namespace EngineDataAccess.Repositories
{
    public class EngineRepository : BaseRepository<Engine, EngineDbContext>, IEngineRepository
    {
        public EngineRepository(EngineDbContext context) : base(context)
        {
        }

        public override Engine? GetById(int id)
        {
            return Context.Set<Engine>().Include(x => x.User).Include(x => x.EngineType).Single(x => x.Id == id);
        }

        public async override Task<Engine?> GetByIdAsync(int id)
        {
            return await Context.Set<Engine>().Include(x => x.User).Include(x => x.EngineType).SingleAsync(x => x.Id == id);
        }
    }
}
