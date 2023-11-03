using EngineModels.OrderModels;
using EngineModels.UserModels;
using System.ComponentModel.DataAnnotations;

namespace EngineModels.EngineModels
{
    public class Engine : IEntity
    {
        public Engine()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [StringLength(20)]
        public string FabricNumber { get; set; }

        public int EngineTypeId { get; set; }

        public EngineType EngineType { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
