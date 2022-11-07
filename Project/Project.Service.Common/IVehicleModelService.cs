using Project.Common;

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
