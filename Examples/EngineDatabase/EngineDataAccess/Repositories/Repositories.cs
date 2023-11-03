namespace EngineDataAccess.Repositories
{
    public class Repositories : IRepositories
    {
        public Repositories(EngineDbContext engineDbContext)
        {
            EngineRepository = new EngineRepository(engineDbContext); 
            OrderRepository = new OrderRepository(engineDbContext);
            EngineTypeRepository = new EngineTypeRepository(engineDbContext);
            UserRepository = new UserRepository(engineDbContext);
        }
        public IEngineRepository EngineRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IEngineTypeRepository EngineTypeRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
