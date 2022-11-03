using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class VehicleModelRepository
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
        public List<VehicleModel> GetAllVehicleModels(QueryParameters query)
        {
            Expression<Func<VehicleModel, bool>> filterExpression = x => x.Name.Equals(query.Filter, StringComparison.InvariantCultureIgnoreCase);
            Expression<Func<VehicleModel, string>> sortExpresion = query.SortBy == SortBy.Name ? x => x.Name : x => x.Abrv;
            var vehicleModel = repository.GetAll(filterExpression, sortExpresion, query);

            return vehicleModel;
        }

        /// <summary>
        /// Returns an entity by id
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned </typeparam>
        /// <param name="id">Entity ID</param>
        /// <returns></returns>
        public async Task<VehicleModel> GetVehicleModel(Guid id)
        {
            var vehicle = await repository.Get<VehicleModel>(id);
            return vehicle;
        }


        /// <summary>
        /// Adds entity to database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be added</param>
        /// <returns>Returns true if entity is added, false otherwise</returns>
        public async Task<bool> CreateVehicleModel(VehicleModel veihlceModel)
        {
            var isCreated = await repository.Create<VehicleModel>(veihlceModel);

            return isCreated;
        }

        /// <summary>
        /// Updates entity in database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Returns true if entity is updated, false otherwise</returns>
        public bool UpdateVehicleModel(VehicleModel veihlceModel)
        {
            var isUpdated = repository.Update<VehicleModel>(veihlceModel);

            return isUpdated;
        }

        /// <summary>
        /// Deletes entity in database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="model">Entity to be deleted</param>
        /// <returns>Returns true if entity is deleted, false otherwise</returns>
        public async Task<bool> DeleteVehicleModel(Guid id)
        {
            var model = await GetVehicleModel(id);
            if (model == null)
            {
                return false;
            }
            var isDelted = repository.Delete<VehicleModel>(model);
            return isDelted;
        }

    }
}
