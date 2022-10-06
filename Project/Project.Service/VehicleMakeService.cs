using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleDbContext vehicleDbContext;
        private readonly IMapper mapper;
        public VehicleMakeService(VehicleDbContext vehicleDbContext, IMapper mapper)
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
        public List<VehicleMakeDTO> GetAllVehicleMakes(QueryParameters query)
        {
            var filterSet = vehicleDbContext.Set<VehicleMake>()
                .AsNoTracking()
                .Where(x => x.Name.Contains(query.Filter));

            Func<VehicleMake, string> sortFunction = Equals(query.SortBy, SortBy.Name) ? x => x.Name : x => x.Abrv;
            var orderedSet = Equals(query.SortOrder, SortOrder.Ascending)  ? filterSet.OrderBy(sortFunction) : filterSet.OrderByDescending(sortFunction);

            var pagedSet = orderedSet
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);
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
            return mapper.Map<VehicleMakeDTO>(make);
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


    }
}
