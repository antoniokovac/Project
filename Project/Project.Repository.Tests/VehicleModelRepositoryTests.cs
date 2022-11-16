using Moq;
using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Tests
{
    public class VehicleModelRepositoryTests
    {
        [Fact]
        public async Task GetAllVehicleModels_ValidRequest_ReturnsList()
        {
            var testMockObject = new TestMockObject();

            var query = new QueryParameters();

            testMockObject.MockGenericRepository
                .Setup(x => x.GetAll(
                    It.IsAny<Expression<Func<VehicleModel, bool>>>(),
                    It.IsAny<Expression<Func<VehicleModel, string>>>(),
                    It.Is<QueryParameters>(x => x == query)))
                .ReturnsAsync(new List<VehicleModel>());

            var allVehicleModels = await testMockObject.VehicleModelRepository.GetAllVehicleModels(query);

            Assert.NotNull(allVehicleModels);
        }

        [Fact]
        public async Task GetVehicleModel_ValidRequest_ReturnsVehicleMake()
        {
            var testMockObject = new TestMockObject();

            var vehicleModelId = Guid.NewGuid();

            testMockObject.MockGenericRepository
                .Setup(x => x.Get<VehicleModel>(vehicleModelId))
                .ReturnsAsync(new VehicleModel { Id = vehicleModelId });
            var vehicleModel = await testMockObject.VehicleModelRepository.GetVehicleModel(vehicleModelId);

            Assert.NotNull(vehicleModel);
            Assert.Equal(vehicleModelId, vehicleModel.Id);
        }
        [Fact]
        public async Task CreateVehicleModel_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleModelInstance = new VehicleModel { Id = Guid.NewGuid() };

            testMockObject.MockGenericRepository
                .Setup(x => x.Create<VehicleModel>(vehicleModelInstance))
                .ReturnsAsync(true);
            var createdVehicleModel = await testMockObject.VehicleModelRepository.CreateVehicleModel(vehicleModelInstance);

            Assert.True(createdVehicleModel);
        }

        [Fact]
        public async Task UpdateVehicleModel_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleModelInstance = new VehicleModel { Id = Guid.NewGuid() };

            testMockObject.MockGenericRepository
                .Setup(x => x.Update<VehicleModel>(vehicleModelInstance))
                .Returns(true);
            var updatedVehicleModel = await testMockObject.VehicleModelRepository.UpdateVehicleModel(vehicleModelInstance);

            Assert.True(updatedVehicleModel);
        }

        [Fact]
        public async Task DeletedVehicleModel_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleModelId = Guid.NewGuid();
            var fetchedVehicleModel = new VehicleModel { Id = vehicleModelId, Abrv = "Abrv", Name = "Name" };

            testMockObject.MockGenericRepository
                .Setup(x => x.Get<VehicleModel>(vehicleModelId))
                .ReturnsAsync(fetchedVehicleModel);
            testMockObject.MockGenericRepository
                .Setup(x => x.Delete<VehicleModel>(fetchedVehicleModel))
                .Returns(true);

            var deletedVehicleModel = await testMockObject.VehicleModelRepository.DeleteVehicleModel(vehicleModelId);

            Assert.True(deletedVehicleModel);
        }

    }
    public class TestMockObject
    {
        public TestMockObject()
        {
            MockGenericRepository = new Mock<IGenericRepository>();
            VehicleModelRepository = new VehicleModelRepository(MockGenericRepository.Object);
        }
        public IVehicleModelRepository VehicleModelRepository { get; }
        public Mock<IGenericRepository> MockGenericRepository { get; }
    }
}


