using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model.DatabaseModels
{
    public class VehicleModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Abrv { get; set; }

        [Required]
        public Guid VehicleMakeId { get; set; }

        [ForeignKey("VehicleMakeId")]
        public VehicleMake VehicleMake { get; set; }

    }
}
