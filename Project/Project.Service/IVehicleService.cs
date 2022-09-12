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
        public List<VehicleMake> GetAllVehicleMakes(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc");

        public Task<VehicleMake> GetVehicleMakeAsync(Guid id);


        public Task<bool> CreateVehicleMakeAsync(VehicleMake vehicleMake);

        public Task<bool> UpdateVehicleMakeAsync(VehicleMake vehicleMake);

        public Task<bool> DeleteVehicleMakeAsync(Guid id);

        // Model CRUD
        public List<VehicleModel> GetAllVehicleModels(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc");

        public Task<VehicleModel> GetVehicleModelAsync(Guid id);

        public Task<bool> CreateVehicleModelAsync(VehicleModel vehicleModel);

        public Task<bool> UpdateVehicleModelAsync(VehicleModel vehicleModel);

        public Task<bool> DeleteVehicleModelAsync(Guid id);

    }
}
