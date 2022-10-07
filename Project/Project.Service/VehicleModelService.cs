using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;

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
        public List<VehicleModelDTO> GetAllVehicleModels(QueryParameters query)
        {
            var filterSet = vehicleDbContext.Set<VehicleModel>()
                .AsNoTracking()
                .Where(x => x.Name.Contains(query.Filter));
            Func<VehicleModel, string> sortFunction = Equals(query.SortBy, SortBy.Name) ? x => x.Name : x => x.Abrv;
            var orderedSet = Equals(query.SortOrder, SortOrder.Ascending) ? filterSet.OrderBy(sortFunction) : filterSet.OrderByDescending(sortFunction);
            var pagedSet = orderedSet
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);
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

