using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDbContext vehicleDbContext;

        public VehicleService(VehicleDbContext vehicleDbContext)
        {
            this.vehicleDbContext = vehicleDbContext;
        }

        /// <summary>
        /// Gets all vehicle makes with filtering, paging and sorting. 
        /// </summary>
        /// <param name="page">Current page.</param>
        /// <param name="pageSize">Given page size.</param>
        /// <param name="filter">Filtering.</param>
        /// <param name="sort">Sort direction.</param>
        /// <returns>Sorted, filtered and paged list of vehicle makes.</returns>
        public List<VehicleMake> GetAllVehicleMakes(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc")
        {
            var filterSet = vehicleDbContext.Set<VehicleMake>()
                .AsNoTracking()
                .Where(x => x.Name.Contains(filter));

            var orderedSet = sort == "asc" ? filterSet.OrderBy(y => y) : filterSet.OrderByDescending(z => z);

            return orderedSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Gets a vehicle make by id.
        /// </summary>
        /// <param name="id">Vehicle make id.</param>
        /// <returns>Vehicle make base model.</returns>
        public async Task<VehicleMake> GetVehicleMakeAsync(Guid id)

        {
            return await vehicleDbContext.Set<VehicleMake>().FindAsync(id);
        }

        /// <summary>
        /// Creates a new vehicle make.
        /// </summary>
        /// <param name="make">Vehicle make object to be saved</param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> CreateVehicleMakeAsync(VehicleMake make)
        {
            await vehicleDbContext.Set<VehicleMake>().AddAsync(make);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Updates a vehicle make.
        /// </summary>
        /// <param name="make"> Vehicle make object needed for update</param>
        /// <returns>True if success or throws excpetion.</returns>
        public async Task<bool> UpdateVehicleMakeAsync(VehicleMake make)
        {
            vehicleDbContext.Set<VehicleMake>().Update(make);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a vehicle make.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> DeleteVehicleMakeAsync(Guid id)
        {
            var make = await vehicleDbContext.Set<VehicleMake>().FindAsync(id);
            vehicleDbContext.Set<VehicleMake>().Remove(make);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Gets all vehicle models with filtering, paging and sorting. 
        /// </summary>
        /// <param name="page">Current page.</param>
        /// <param name="pageSize">Given page size.</param>
        /// <param name="filter">Filtering.</param>
        /// <param name="sort">Sort direction.</param>
        /// <returns>Sorted, filtered and paged list of vehicle models.</returns>

        public List<VehicleModel> GetAllVehicleModels(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc")
        {
            var filterSet = vehicleDbContext.Set<VehicleModel>()
                .AsNoTracking()
                .Where(x => x.VehicleMake.Name.Contains(filter));

            var orderedSet = sort == "asc" ? filterSet.OrderBy(y => y) : filterSet.OrderByDescending(z => z);

            return orderedSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Gets a vehicle model by id.
        /// </summary>
        /// <param name="id">Vehicle make id.</param>
        /// <returns>Vehicle model base model.</returns>
        public async Task<VehicleModel> GetVehicleModelAsync(Guid id)
        {
            return await vehicleDbContext.Set<VehicleModel>().FindAsync(id);
        }

        /// <summary>
        /// Creates a new vehicle model.
        /// </summary>
        /// <param name="model">Vehicle model object to be saved</param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> CreateVehicleModelAsync(VehicleModel model)
        {
            await vehicleDbContext.Set<VehicleModel>().AddAsync(model);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Updates a vehicle model.
        /// </summary>
        /// <param name="model"> Vehicle model object needed for update</param>
        /// <returns>True if success or throws excpetion.</returns>
        public async Task<bool> UpdateVehicleModelAsync(VehicleModel model)
        {
            vehicleDbContext.Set<VehicleModel>().Update(model);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a vehicle model.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> DeleteVehicleModelAsync(Guid id)
        {
            var model = await vehicleDbContext.Set<VehicleModel>().FindAsync(id);
            vehicleDbContext.Set<VehicleModel>().Remove(model);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }
    }
}
