namespace EngineDataAccess.Repositories
{
    public interface IRepositories
    {
        IEngineRepository EngineRepository { get; }
        IEngineTypeRepository EngineTypeRepository { get; }
        IOrderRepository OrderRepository { get; }
        IUserRepository UserRepository { get; }
    }
}