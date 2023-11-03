using EngineModels.EngineModels;
using EngineModels.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace EngineDataAccess.Repositories
{
    public class OrderRepository : BaseRepository<Order, EngineDbContext>, IOrderRepository
    {
        public OrderRepository(EngineDbContext context) : base(context)
        {
        }

        public override Order? GetById(int id)
        {
            return Context.Set<Order>().
                Include(x => x.RepairType).
                Include(x => x.Engine).
                ThenInclude(e => e.User).
                Include(e => e.Engine.EngineType).
                Single(x => x.Id == id);
        }

        public async override Task<Order?> GetByIdAsync(int id)
        {
            return await Context.Set<Order>().
                Include(x => x.RepairType).
                Include(x => x.Engine).
                ThenInclude(e => e.User).
                Include(e => e.Engine.EngineType).
                SingleAsync(x => x.Id == id);
        }
    }
}
