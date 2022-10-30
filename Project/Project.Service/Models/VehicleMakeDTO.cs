using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class VehicleMakeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
