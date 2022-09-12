using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System.Security.Cryptography.X509Certificates;

namespace Project.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDbContext vehicleDbContext;
        private readonly IMapper mapper;

        public VehicleService(VehicleDbContext vehicleDbContext, IMapper mapper)
        {
            this.vehicleDbContext = vehicleDbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all vehicle makes with filtering, paging and sorting. 
        /// </summary>
        /// <param name="page">Current page.</param>
        /// <param name="pageSize">Given page size.</param>
        /// <param name="filter">Filtering.</param>
        /// <param name="sort">Sort direction.</param>
        /// <returns>Sorted, filtered and paged list of vehicle makes.</returns>
        public List<VehicleMakeDTO> GetAllVehicleMakes(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc")
        {
            var filterSet = vehicleDbContext.Set<VehicleMake>()
                .AsNoTracking()
                .Where(x => x.Name.Contains(filter));

            var orderedSet = sort == "asc" ? filterSet.OrderBy(y => y.Name) : filterSet.OrderByDescending(z => z.Name);

            var pagedSet = orderedSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return mapper.Map<List<VehicleMakeDTO>>(pagedSet);
        }

        /// <summary>
        /// Gets a vehicle make by id.
        /// </summary>
        /// <param name="id">Vehicle make id.</param>
        /// <returns>Vehicle make base model.</returns>
        public async Task<VehicleMakeDTO> GetVehicleMakeAsync(Guid id)
        {
            var make = await vehicleDbContext.Set<VehicleMake>().FindAsync(id);
            return  mapper.Map<VehicleMakeDTO>(make);
        }

        /// <summary>
        /// Creates a new vehicle make.
        /// </summary>
        /// <param name="make">Vehicle make object to be saved</param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> CreateVehicleMakeAsync(VehicleMakeDTO makeDto)
        {
            var make = mapper.Map<VehicleMake>(makeDto);
            await vehicleDbContext.Set<VehicleMake>().AddAsync(make);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Updates a vehicle make.
        /// </summary>
        /// <param name="make"> Vehicle make object needed for update</param>
        /// <returns>True if success or throws excpetion.</returns>
        public async Task<bool> UpdateVehicleMakeAsync(VehicleMakeDTO makeDto)
        {
            var make = mapper.Map<VehicleMake>(makeDto);
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

        public List<VehicleModelDTO> GetAllVehicleModels(
            int page = 1,
            int pageSize = 10,
            string filter = "",
            string sort = "asc")
        {
            var filterSet = vehicleDbContext.Set<VehicleModel>()
                .AsNoTracking()
                .Where(x => x.VehicleMake.Name.Contains(filter));

            var orderedSet = sort == "asc" ? filterSet.OrderBy(y => y.Name) : filterSet.OrderByDescending(z => z.Name);

            var pagedSet = orderedSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return mapper.Map<List<VehicleModelDTO>>(pagedSet);
        }

        /// <summary>
        /// Gets a vehicle model by id.
        /// </summary>
        /// <param name="id">Vehicle make id.</param>
        /// <returns>Vehicle model base model.</returns>
        public async Task<VehicleModelDTO> GetVehicleModelAsync(Guid id)
        {
            var model = await vehicleDbContext.Set<VehicleModel>().FindAsync(id);
            return mapper.Map<VehicleModelDTO>(model);
        }

        /// <summary>
        /// Creates a new vehicle model.
        /// </summary>
        /// <param name="model">Vehicle model object to be saved</param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> CreateVehicleModelAsync(VehicleModelDTO modelDto)
        {
            var model = mapper.Map<VehicleModel>(modelDto);
            await vehicleDbContext.Set<VehicleModel>().AddAsync(model);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Updates a vehicle model.
        /// </summary>
        /// <param name="model"> Vehicle model object needed for update</param>
        /// <returns>True if success or throws excpetion.</returns>
        public async Task<bool> UpdateVehicleModelAsync(VehicleModelDTO modelDto)
        {
            var model = mapper.Map<VehicleModel>(modelDto);
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
