using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleMakeService
    {
        public List<VehicleMakeDTO> GetAllVehicleMakes( QueryParameters query);

        public Task<VehicleMakeDTO> GetVehicleMakeAsync(Guid id);
        public Task<bool> CreateVehicleMakeAsync(VehicleMakeDTO makeDto);
        public Task<bool> UpdateVehicleMakeAsync(VehicleMakeDTO makeDto);
        public Task<bool> DeleteVehicleMakeAsync(Guid id);
    }
}
