using EngineModels.EngineModels;
using EngineModels.UserModels;
using Microsoft.EntityFrameworkCore;

namespace EngineDataAccess.Repositories
{
    public class UserRepository : BaseRepository<User, EngineDbContext>, IUserRepository
    {
        public UserRepository(EngineDbContext context) : base(context)
        {
        }

        public override User? GetById(int id)
        {
            return Context.Set<User>().Include(x => x.Engines).FirstOrDefault(x => x.Id == id);
        }

        public async override Task<User?> GetByIdAsync(int id)
        {
            return await Context.Set<User>().Include(x => x.Engines).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
