using EngineModels.EngineModels;
using System.ComponentModel.DataAnnotations;

namespace EngineModels.OrderModels
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EngineId { get; set; }
        public Engine Engine { get; set; }

        [Required]
        public RepairType RepairType { get; set; }

        [Required]
        public string Description { get; set; }

        public OrderState State { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
