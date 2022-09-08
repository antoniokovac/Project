using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("MakeId")]
        public int MakeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }
        public VehicleMake VehicleMake { get; set; }

    }
}
