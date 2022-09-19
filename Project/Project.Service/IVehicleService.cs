using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleService
    {
        // Make CRUD
        public List<VehicleMakeDTO> GetAllVehicleMakes(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc");

        public Task<VehicleMakeDTO> GetVehicleMakeAsync(Guid id);
        public Task<bool> CreateVehicleMakeAsync(VehicleMakeDTO makeDto);
        public Task<bool> UpdateVehicleMakeAsync(VehicleMakeDTO makeDto);
        public Task<bool> DeleteVehicleMakeAsync(Guid id);

        // Model CRUD
        public List<VehicleModelDTO> GetAllVehicleModels(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc");

        public Task<VehicleModelDTO> GetVehicleModelAsync(Guid id);
        public Task<bool> CreateVehicleModelAsync(VehicleModelDTO modelDto);
        public Task<bool> UpdateVehicleModelAsync(VehicleModelDTO modelDto);
        public Task<bool> DeleteVehicleModelAsync(Guid id);
    }
}
