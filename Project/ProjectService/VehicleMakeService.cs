using AutoMapper;
using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository repository;
        private readonly IMapper mapper;

        public VehicleMakeService(IVehicleMakeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public List<VehicleMakeDTO> GetAllVehicleMakes(QueryParameters query) 
        {
            var getAll = repository.GetAllVehicleMakes(query);
            return mapper.Map<List<VehicleMakeDTO>>(getAll);
        }
        public async Task<VehicleMakeDTO> GetVehicleMake(Guid id) 
        {
            var getVehicle =  await repository.GetVehicleMake(id);
            return mapper.Map<VehicleMakeDTO>(getVehicle);
        }

        public async Task<bool> CreateVehicleMake(VehicleMakeDTO vehicleMake) 
        {
            var vehicleMakeDTO = mapper.Map<VehicleMake>(vehicleMake);
            return await repository.CreateVehicleMake(vehicleMakeDTO);
            
        }
    
        public async Task<bool> UpdateVehicleMake(VehicleMakeDTO vehicleMake) 
        {
            var vehicleMakeDTO = mapper.Map<VehicleMake>(vehicleMake);
            return await repository.UpdateVehicleMake(vehicleMakeDTO);
        }

        public Task<bool> DeleteVehicleMake(Guid id) 
        {
           return repository.DeleteVehicleMake(id);
        }
    }
}
