using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using System.Linq.Expressions;

namespace Project.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly IGenericRepository repository;

        public VehicleModelRepository(IGenericRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Returns paged, sorted and filtered list of database entities.
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="filter">Filter expression</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="page">Page number</param>
        /// <param name="sort">Sort expression</param>
        /// <returns></returns>
        public async Task<List<VehicleModel>> GetAllVehicleModels(QueryParameters query)
        {
            Expression<Func<VehicleModel, bool>> filterExpression = x => x.Name.ToLower().Contains(query.Filter.ToLower());
            Expression<Func<VehicleModel, string>> sortExpresion = query.SortBy == SortBy.Name ? x => x.Name : x => x.Abrv;
            var vehicleModels = await repository.GetAll(filterExpression, sortExpresion, query);

            return vehicleModels;
        }

        /// <summary>
        /// Returns an entity by id
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned </typeparam>
        /// <param name="id">Entity ID</param>
        /// <returns></returns>
        public async Task<VehicleModel> GetVehicleModel(Guid id)
        {
            var vehicle = await repository.GetAsync<VehicleModel>(id);
            return vehicle;
        }


        /// <summary>
        /// Adds entity to database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be added</param>
        /// <returns>Returns true if entity is added, false otherwise</returns>
        public async Task<bool> CreateVehicleModel(VehicleModel vehicleModel)
        {
            using (var unitOfWork = new UnitOfWork(repository))
            {
                var isCreated = await unitOfWork.AddAsync<VehicleModel>(vehicleModel);
                await unitOfWork.SaveChangesAsync();
                return isCreated;
            }
        }

        public async Task<bool> UpdateVehicleModel(VehicleModel vehicleModel)
        {
            using (var unitOfWork = new UnitOfWork(repository))
            {
                var isUpdated = unitOfWork.Update<VehicleModel>(vehicleModel);
                await unitOfWork.SaveChangesAsync();
                return isUpdated;
            }
        }

        public async Task<bool> DeleteVehicleModel(Guid id)
        {
            using (var unitOfWork = new UnitOfWork(repository))
            {
                var model = await GetVehicleModel(id);
                if (model is null)
                {
                    return false;
                }
                var isDelted = unitOfWork.Delete<VehicleModel>(model);
                await unitOfWork.SaveChangesAsync();
                return isDelted;
            }
        }

    }
}
