using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using Project.Service.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleDbContext vehicleDbContext;
        private readonly IMapper mapper;
        public VehicleModelService(VehicleDbContext vehicleDbContext, IMapper mapper)
        {
            this.vehicleDbContext = vehicleDbContext;
            this.mapper = mapper;
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
            Filtering filter,
            Paging paging,
            Sorting sort)
        {
            var filterSet = vehicleDbContext.Set<VehicleModel>()
                .AsNoTracking()
                .Where(x => x.VehicleMake.Name.Contains(filter.Filter));

            var orderedSet = Equals(sort.Sort, SortDirection.Ascending) ? filterSet.OrderBy(y => y.Name) : filterSet.OrderByDescending(z => z.Name);

            var pagedSet = orderedSet
                .Skip((paging.Page - 1) * paging.PageSize)
                .Take(paging.PageSize);
            return mapper.Map<List<VehicleModelDTO>>(pagedSet);
        }

        /// <summary>
        /// Gets a vehicle model by id.
        /// </summary>
        /// <param name="id">Vehicle make id.</param>
        /// <returns>Vehicle model base model.</returns>
        public async Task<VehicleModelDTO> GetVehicleModelAsync(Guid id)
        {
            var model = await vehicleDbContext.Set<VehicleModelService>().FindAsync(id);
            return mapper.Map<VehicleModelDTO>(model);
        }

        /// <summary>
        /// Creates a new vehicle model.
        /// </summary>
        /// <param name="model">Vehicle model object to be saved</param>
        /// <returns>True if success or throws excpetion</returns>
        public async Task<bool> CreateVehicleModelAsync(VehicleModelDTO modelDto)
        {
            var model = mapper.Map<VehicleModelService>(modelDto);
            await vehicleDbContext.Set<VehicleModelService>().AddAsync(model);
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
            var model = mapper.Map<VehicleModelService>(modelDto);
            vehicleDbContext.Set<VehicleModelService>().Update(model);
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
            var model = await vehicleDbContext.Set<VehicleModelService>().FindAsync(id);
            vehicleDbContext.Set<VehicleModelService>().Remove(model);
            await vehicleDbContext.SaveChangesAsync();
            return true;
        }
    }
}

