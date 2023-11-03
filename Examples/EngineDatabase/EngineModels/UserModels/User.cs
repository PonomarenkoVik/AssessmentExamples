using EngineModels.EngineModels;
using EngineModels.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineModels.UserModels
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)] 
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        //[Column("Surname")]
        public string LastName { get; set; }

        public IEnumerable<Engine> Engines { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
