using System.ComponentModel.DataAnnotations;

namespace Project.Model.DatabaseModels
{
    public class VehicleMake
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Abrv { get; set; }

    }
}
