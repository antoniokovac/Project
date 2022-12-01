using Project.Common;

namespace Project.Service.Common
{
    public interface IVehicleMakeService
    {
        public Task<List<VehicleMakeDTO>> GetAllVehicleMakes(QueryParameters query);
        public Task<VehicleMakeDTO> GetVehicleMake(Guid id);

        public Task<bool> CreateVehicleMake(VehicleMakeDTO vehicleMake);

        public Task<bool> UpdateVehicleMake(VehicleMakeDTO vehicleMake);

        public Task<bool> DeleteVehicleMake(Guid id);

    }
}
