using EngineModels.EngineModels;

namespace EngineDataAccess.Repositories
{
    public class EngineTypeRepository : BaseRepository<EngineType, EngineDbContext>, IEngineTypeRepository
    {
        public EngineTypeRepository(EngineDbContext context) : base(context)
        {
        }
    }
}
