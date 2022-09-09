using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDbContext vehicleMake;
        private readonly VehicleDbContext vehicleModel;

        public VehicleService(VehicleDbContext vehicleMake, VehicleDbContext vehicleModel)
        {
            this.vehicleMake = vehicleMake;
            this.vehicleModel = vehicleModel;
        }



        // Make CRUD
        public List<VehicleMake> GetAllVehicleMakes()
        {
            return vehicleMake.Set<VehicleMake>().ToList();
        }

        public async Task<VehicleMake> GetVehicleMakeAsync(Guid id)

        {
            return await vehicleMake.Set<VehicleMake>().FindAsync(id);
        }
        public async Task<bool> CreateVehicleMakeAsync(VehicleMake make)
        {
            await vehicleMake.Set<VehicleMake>().AddAsync(make);
            await vehicleMake.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVehicleMakeAsync(VehicleMake make)
        {
            vehicleMake.Set<VehicleMake>().Update(make);
            await vehicleMake.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteVehicleMakeAsync(Guid id)
        {
            var make = await vehicleMake.Set<VehicleMake>().FindAsync(id);
            vehicleMake.Set<VehicleMake>().Remove(make);
            await vehicleMake.SaveChangesAsync();
            return true;
        }

        // Model CRUD
        public List<VehicleModel> GetAllVehicleModels()
        {
            return vehicleModel.Set<VehicleModel>().ToList();
        }

        public async Task<VehicleModel> GetVehicleModelAsync(Guid id)
        {
            return await vehicleModel.Set<VehicleModel>().FindAsync(id);
        }

        public async Task<bool> CreateVehicleModelAsync(VehicleModel model)
        {
            await vehicleModel.Set<VehicleModel>().AddAsync(model);
            await vehicleModel.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVehicleModelAsync(VehicleModel model)
        {
            vehicleModel.Set<VehicleModel>().Update(model);
            await vehicleModel.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleModelAsync(Guid id)
        {
            var model = await vehicleModel.Set<VehicleModel>().FindAsync(id);
            vehicleModel.Set<VehicleModel>().Remove(model);
            await vehicleModel.SaveChangesAsync();
            return true;
        }

    }
}
