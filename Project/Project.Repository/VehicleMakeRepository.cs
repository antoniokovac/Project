using Microsoft.EntityFrameworkCore;
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
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly IGenericRepository repository;

        public VehicleMakeRepository(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public List<VehicleMake> GetAllVehicleMakes(QueryParameters query)
        {
            Expression<Func<VehicleMake, bool>> filterExpression = x => x.Name.Equals(query.Filter, StringComparison.InvariantCultureIgnoreCase);
            Expression<Func<VehicleMake, string>> sortExpresion = query.SortBy == SortBy.Name ? x => x.Name : x => x.Abrv;
            var vehicleMakes = repository.GetAll(filterExpression, sortExpresion, query);

            return vehicleMakes;
        }

        public async Task<VehicleMake> GetVehicleMake(Guid id)
        {
            var vehicle = await repository.Get<VehicleMake>(id);
            return vehicle;
        }
    
        public async Task<bool> CreateVehicleMake(VehicleMake vehicleMake)
        {
            var isCreated = await repository.Create<VehicleMake>(vehicleMake);

            return isCreated;
        }

        public bool UpdateVehicleMake(VehicleMake vehicleMake)
        {
            var isUpdated = repository.Update<VehicleMake>(vehicleMake);

            return isUpdated;
        }

        public async Task<bool> DeleteVehicleMake(Guid id) 
        {
            var make = await GetVehicleMake(id);
            if (make == null)
            {
                return false;
            }
            var isDelted =  repository.Delete<VehicleMake>(make);
            return isDelted;
        }

    }
}

