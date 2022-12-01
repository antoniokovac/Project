using AutoMapper;
using Moq;
using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Service.Tests.VehicleModeServiceTests
{
    public class VehicleModelServiceTests
    {

        [Fact]
        public async Task GetAllVehicleModels_ValidRequest_ReturnsList()
        {
            var testMockObject = new TestMockObject();
            var databaseResponse = new List<VehicleModel>();
            var query = new QueryParameters();
            testMockObject.MockVehicleModelRepository
                .Setup(x => x.GetAllVehicleModels(query))
                .ReturnsAsync(databaseResponse);
            testMockObject.MockMapper
                .Setup(x => x.Map<List<VehicleModelDTO>>(databaseResponse))
                .Returns(new List<VehicleModelDTO>());

            var allVehicleModels = await testMockObject.VehicleModelService.GetAllVehicleModels(query);

            Assert.NotNull(allVehicleModels);
        }

        [Fact]
        public async Task GetVehicleModel_ValidRequest_ReturnsVehicleModelDTO()
        {
            var testMockObject = new TestMockObject();
            var vehicleModelId = Guid.NewGuid();
            var databaseResponse = new VehicleModel { Id = vehicleModelId };

            testMockObject.MockVehicleModelRepository
                .Setup(x => x.GetVehicleModel(vehicleModelId))
                .ReturnsAsync(databaseResponse);

            testMockObject.MockMapper
                .Setup(x => x.Map<VehicleModelDTO>(databaseResponse))
                .Returns(new VehicleModelDTO { Id = vehicleModelId });

            var vehicleModel = await testMockObject.VehicleModelService.GetVehicleModel(vehicleModelId);

            Assert.NotNull(vehicleModel);
            Assert.Equal(vehicleModelId, vehicleModel.Id);
        }
        [Fact]
        public async Task CreateVehicleModel_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleModelDTOInstance = new VehicleModelDTO() { Abrv = "a", Name = "n" };
            var vehicleModelInstance = new VehicleModel();

            testMockObject.MockVehicleModelRepository
                .Setup(x => x.CreateVehicleModel(vehicleModelInstance))
                .ReturnsAsync(true);
            testMockObject.MockMapper
                .Setup(x => x.Map<VehicleModel>(vehicleModelDTOInstance))
                .Returns(vehicleModelInstance);

            var createdVehicleModel = await testMockObject.VehicleModelService.CreateVehicleModel(vehicleModelDTOInstance);

            Assert.True(createdVehicleModel);
        }

        [Fact]
        public async Task UpdateVehicleModel_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleModelDTOInstance = new VehicleModelDTO() { Abrv = "a", Name = "n" };
            var vehicleModelInstance = new VehicleModel();

            testMockObject.MockVehicleModelRepository
                .Setup(x => x.UpdateVehicleModel(vehicleModelInstance))
                .ReturnsAsync(true);

            testMockObject.MockMapper
                .Setup(x => x.Map<VehicleModel>(vehicleModelDTOInstance))
                .Returns(vehicleModelInstance);
            var updatedVehicleModel = await testMockObject.VehicleModelService.UpdateVehicleModel(vehicleModelDTOInstance);

            Assert.True(updatedVehicleModel);
        }

        [Fact]
        public void DeleteVehicleModel_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();
            var vehicleModelId = Guid.NewGuid();
            var databaseResponse = new VehicleModel { Id = vehicleModelId };

            testMockObject.MockVehicleModelRepository
                .Setup(x => x.DeleteVehicleModel(vehicleModelId))
                .ReturnsAsync(true);

            var vehicleModel = testMockObject.VehicleModelService.DeleteVehicleModel(vehicleModelId);

            Assert.True(vehicleModel.Result);
        }
    }
    public class TestMockObject
    {
        public TestMockObject()
        {
            MockVehicleModelRepository = new Mock<IVehicleModelRepository>();
            MockMapper = new Mock<IMapper>();
            VehicleModelService = new VehicleModelService(MockVehicleModelRepository.Object, MockMapper.Object);
        }
        public IVehicleModelService VehicleModelService { get; }
        public Mock<IVehicleModelRepository> MockVehicleModelRepository { get; }

        public Mock<IMapper> MockMapper { get; }
    }
}
