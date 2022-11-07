using Project.Common;
using Project.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        public Task<List<VehicleMake>> GetAllVehicleMakes(QueryParameters query);
        public Task<VehicleMake> GetVehicleMake(Guid id);

        public Task<bool> CreateVehicleMake(VehicleMake vehicleMake);


        public Task<bool> UpdateVehicleMake(VehicleMake vehicleMake) ;


        public Task<bool> DeleteVehicleMake(Guid id) ;
    }
}
