using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using System.Linq.Expressions;

namespace Project.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly IGenericRepository repository;
        public VehicleMakeRepository(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<VehicleMake>> GetAllVehicleMakes(QueryParameters query)
        {
            Expression<Func<VehicleMake, bool>> filterExpression = x => x.Name.ToLower().Contains(query.Filter.ToLower());
            Expression<Func<VehicleMake, string>> sortExpresion = query.SortBy == SortBy.Name ? x => x.Name : x => x.Abrv;
            var vehicleMakes = await repository.GetAll(filterExpression, sortExpresion, query);

            return vehicleMakes;
        }

        public async Task<VehicleMake> GetVehicleMake(Guid id)
        {
            var vehicle = await repository.Get<VehicleMake>(id);
            return vehicle;
        }

        public async Task<bool> CreateVehicleMake(VehicleMake vehicleMake)
        {
            using (var unitOfWork = new UnitOfWork(repository))
            {
                var isCreated = await unitOfWork.AddAsync<VehicleMake>(vehicleMake);
                await unitOfWork.SaveChangesAsync();
                return isCreated;
            }
        }

        public async Task<bool> UpdateVehicleMake(VehicleMake vehicleMake)
        {
            using (var unitOfWork = new UnitOfWork(repository))
            {
                var isUpdated = unitOfWork.Update<VehicleMake>(vehicleMake);
                await unitOfWork.SaveChangesAsync();
                return isUpdated;
            }
        }

        public async Task<bool> DeleteVehicleMake(Guid id)
        {
            using (var unitOfWork = new UnitOfWork(repository))
            {
                var make = await GetVehicleMake(id);
                if (make is null)
                {
                    return false;
                }
                var isDelted = unitOfWork.Delete<VehicleMake>(make);
                await unitOfWork.SaveChangesAsync();
                return isDelted;
            }
        }

    }
}

