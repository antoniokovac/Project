using Project.Common;
using Project.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IVehicleModelRepository
    {
        public Task<List<VehicleModel>> GetAllVehicleModels(QueryParameters query);
        public Task<VehicleModel> GetVehicleModel(Guid id);

        public Task<bool> CreateVehicleModel(VehicleModel vehicleMake);


        public Task<bool> UpdateVehicleModel(VehicleModel vehicleMake);


        public Task<bool> DeleteVehicleModel(Guid id);
    }
}
