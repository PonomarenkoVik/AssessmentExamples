using System.ComponentModel.DataAnnotations;

namespace EngineModels.EngineModels
{
    public class EngineType : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayType { get; set; }

        public float EnginePower { get; set; }

        public int EngineSpeed { get; set; }

        public float? Weight { get; set; }

        public float? Height { get; set; }

        public float? Width { get; set; }
    }
}
