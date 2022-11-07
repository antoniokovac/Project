using AutoMapper;
using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository repository;
        private readonly IMapper mapper;

        public VehicleModelService(IVehicleModelRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<VehicleModelDTO>> GetAllVehicleModels(QueryParameters query)
        {
            var getAll = await repository.GetAllVehicleModels(query);
            return mapper.Map<List<VehicleModelDTO>>(getAll);
        }
        public async Task<VehicleModelDTO> GetVehicleModel(Guid id)
        {
            var getVehicle = await repository.GetVehicleModel(id);
            return mapper.Map<VehicleModelDTO>(getVehicle);
        }

        public async Task<bool> CreateVehicleModel(VehicleModelDTO vehicleModel)
        {
            vehicleModel.Id = Guid.NewGuid();
            var vehicleModelDTO = mapper.Map<VehicleModel>(vehicleModel);
            return await repository.CreateVehicleModel(vehicleModelDTO);

        }

        public async Task<bool> UpdateVehicleModel(VehicleModelDTO vehicleModel)
        {
            var vehicleModelDTO = mapper.Map<VehicleModel>(vehicleModel);
            return await repository.UpdateVehicleModel(vehicleModelDTO);
        }

        public Task<bool> DeleteVehicleModel(Guid id)
        {
            return repository.DeleteVehicleModel(id);
        }
    }
}
