using EngineDataAccess.Repositories;
using EngineModels.EngineModels;
using EngineModels.OrderModels;
using EngineModels.UserModels;
using EngineUIComponents.ViewModels.LookupItems;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EngineUIComponents.ViewModels.Helpers
{
    public static class LookupItemExtensions
    {
        public static IQueryable<EngineLookupItem> GetLookupItems(this IEngineRepository repository)
        {
            return repository.GetAll().Include(x => x.Orders).Include(x => x.User).Include(x => x.EngineType).Select(x => x.ToLookupItem());
        }

        public static EngineLookupItem ToLookupItem(this Engine x)
        {
            return new EngineLookupItem()
            {
                Id = x.Id,
                EngineType = x.EngineType.DisplayType,
                FabricNumber = x.FabricNumber,
                NumberOfOrders = x.Orders.Count(),
                Owner = $"{x.User.FirstName} {x.User.LastName}"
            };
        }

        public static IQueryable<EngineTypeLookupItem> GetLookupItems(this IEngineTypeRepository repository)
        {
            return repository.GetAll().Select(x => x.ToLookupItem());
        }

        public static EngineTypeLookupItem ToLookupItem( this EngineType x)
        {
            return new EngineTypeLookupItem()
            {
                Id = x.Id,
                Type = x.Type,
                EnginePower = x.EnginePower,
                EngineSpeed = x.EngineSpeed,
                Weight = x.Weight,
                Height = x.Weight,
                Width = x.Weight
            };
        }

        public static IQueryable<OrderLookupItem> GetLookupItems(this IOrderRepository orderRepository, IUserRepository userRepository)
        {
            return orderRepository.GetAll().Include(o => o.Engine).ThenInclude(e => e.User).Include(o => o.Engine.EngineType).Select(o => o.ToLookupItem(userRepository));
        }

        public static OrderLookupItem ToLookupItem(this Order o, IUserRepository userRepository)
        {
            return new OrderLookupItem()
            {
                Id = o.Id,
                Creator = $"{userRepository.GetAll().First(u => u.Id == o.UserId).FirstName} {userRepository.GetAll().First(u => u.Id == o.UserId).LastName}",
                EngineOwner = $"{o.Engine.User.FirstName} {o.Engine.User.LastName}",
                Description = o.Description,
                EngineDisplayType = o.Engine.EngineType.DisplayType,
                EngineId = o.EngineId,
                RepairType = o.RepairType,
                State = o.State
            };
        }

        public static IQueryable<UserLookupItem> GetLookupItems(this IUserRepository userRepository, IOrderRepository orderRepository)
        {
            return userRepository.GetAll().Include(x => x.Engines).Select(x => x.ToLookupItem(orderRepository));
        }

        public static UserLookupItem ToLookupItem(this User x, IOrderRepository orderRepository)
        {
            return new UserLookupItem()
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                EnginesCount = x.Engines.Count(),
                ReadyOrdersCount = orderRepository.GetAll().Where(o => o.UserId == x.Id).Where(y => y.State == EngineModels.OrderModels.OrderState.Repaired || y.State == EngineModels.OrderModels.OrderState.Disposed).Count(),
                OrdersCount = orderRepository.GetAll().Where(o => o.UserId == x.Id).Count()
            };
        }
    }
}
