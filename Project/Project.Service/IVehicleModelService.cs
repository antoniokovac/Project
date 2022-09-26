using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleModelService
    {
        public List<VehicleModelDTO> GetAllVehicleModels(
            Filtering filter,
            Paging paging,
            Sorting sort;

        public Task<VehicleModelDTO> GetVehicleModelAsync(Guid id);
        public Task<bool> CreateVehicleModelAsync(VehicleModelDTO modelDto);
        public Task<bool> UpdateVehicleModelAsync(VehicleModelDTO modelDto);
        public Task<bool> DeleteVehicleModelAsync(Guid id);
    }
}
