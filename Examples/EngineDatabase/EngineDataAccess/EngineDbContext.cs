using EngineModels.EngineModels;
using EngineModels.OrderModels;
using EngineModels.UserModels;
using Microsoft.EntityFrameworkCore;

namespace EngineDataAccess
{
    public class EngineDbContext : DbContext
    {
        public EngineDbContext(): base(new DbContextOptionsBuilder<EngineDbContext>().
            UseSqlServer("Server=(local);Database=EngineDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;").Options)
        {
                
        }

        public EngineDbContext(DbContextOptions<EngineDbContext> options) : base(options)
        {

        }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<EngineType> EngineTypes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<User>().HasData(
        //       new List<User>()
        //       {
        //            new User() { Id = 1, FirstName = "Viktor", LastName="Ponomarenko" },
        //            new User() { Id = 2, FirstName = "Oleksiy", LastName="Babkin" },
        //            new User() { Id = 3, FirstName = "Serhiy", LastName="Lisovets" }
        //       });

        //    modelBuilder.Entity<EngineType>().HasData(
        //      new List<EngineType>()
        //      {
        //            new EngineType() { Id = 1, DisplayType = "АИР200м4, 37 kWt, 1500 1/s", EnginePower = 37, EngineSpeed = 1500, Type = "АИР200м4" },
        //            new EngineType() { Id = 2, DisplayType = "АИМ132м2, 11 kWt, 2900 1/s", EnginePower = 11, EngineSpeed = 2900, Type = "АИМ132м2"  },
        //            new EngineType() { Id = 3, DisplayType = "АИИ160S6, 11 kWt, 1000 1/s", EnginePower = 11, EngineSpeed = 1000, Type = "АИИ160S6" }
        //      });

        //    modelBuilder.Entity<Engine>().HasData(
        //        new List<Engine>()
        //        {
        //            new Engine() { Id = 1, EngineTypeId = 1, FabricNumber = "F45GT76765", Orders = new List<Order>(), UserId = 1 },
        //            new Engine() { Id = 2, EngineTypeId = 2, FabricNumber = "FFGHJH7765", Orders = new List<Order>(), UserId = 1 },
        //            new Engine() { Id = 3, EngineTypeId = 2, FabricNumber = "FGJKLD6578", Orders = new List<Order>(), UserId = 1 }
        //        });

        //    modelBuilder.Entity<Order>().HasData(
        //       new List<Order>()
        //       {
        //            new Order() { Id = 1, EngineId = 1, UserId = 1, Description = "Common repairment", RepairType = RepairType.Maintenance, State = OrderState.Created },
        //            new Order() { Id = 2, EngineId = 2, UserId = 1, Description = "Medium repairment", RepairType = RepairType.Maintenance, State = OrderState.InWork },
        //            new Order() { Id = 3, EngineId = 3, UserId = 1, Description = "Overhaul repairment", RepairType = RepairType.Overhaul, State = OrderState.Repaired },
        //       });
        //}
    }
}
