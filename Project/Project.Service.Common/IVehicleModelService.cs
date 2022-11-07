using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IVehicleModelService
    {
        public Task<List<VehicleModelDTO>> GetAllVehicleModels(QueryParameters query);

        public Task<VehicleModelDTO> GetVehicleModel(Guid id);

        public Task<bool> CreateVehicleModel(VehicleModelDTO vehicleModel);

        public Task<bool> UpdateVehicleModel(VehicleModelDTO vehicleModel);

        public Task<bool> DeleteVehicleModel(Guid id);

    }
}
