using Moq;
using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using System.Linq.Expressions;

namespace Project.Repository.Tests.VehicleMakeRepositoryTest
{
    public class VehicleMakeRepositoryTest
    {
        [Fact]
        public async Task GetAllVehicleMakes_ValidRequest_ReturnsList()
        {
            var testMockObject = new TestMockObject();

            var query = new QueryParameters();

            testMockObject.MockGenericRepository
                .Setup(x => x.GetAll(
                    It.IsAny<Expression<Func<VehicleMake, bool>>>(),
                    It.IsAny<Expression<Func<VehicleMake, string>>>(),
                    It.Is<QueryParameters>(x => x == query)))
                .ReturnsAsync(new List<VehicleMake>());

            var allVehicleMakes = await testMockObject.VehicleMakeRepository.GetAllVehicleMakes(query);

            Assert.NotNull(allVehicleMakes);
        }

        [Fact]
        public async Task GetVehicleMake_ValidRequest_ReturnsVehicleMake()
        {
            var testMockObject = new TestMockObject();

            var vehicleMakeId = Guid.NewGuid();

            testMockObject.MockGenericRepository
                .Setup(x => x.Get<VehicleMake>(vehicleMakeId))
                .ReturnsAsync(new VehicleMake { Id = vehicleMakeId });
            var vehicleMake = await testMockObject.VehicleMakeRepository.GetVehicleMake(vehicleMakeId);

            Assert.NotNull(vehicleMake);
            Assert.Equal(vehicleMakeId, vehicleMake.Id);
        }
        [Fact]
        public async Task CreateVehicleMake_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleMakeInstance = new VehicleMake { Id = Guid.NewGuid() };

            testMockObject.MockGenericRepository
                .Setup(x => x.Create<VehicleMake>(vehicleMakeInstance))
                .ReturnsAsync(true);
            var createdVehicleMake = await testMockObject.VehicleMakeRepository.CreateVehicleMake(vehicleMakeInstance);

            Assert.True(createdVehicleMake);
        }

        [Fact]
        public async Task UpdateVehicleMake_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleMakeInstance = new VehicleMake { Id = Guid.NewGuid() };

            testMockObject.MockGenericRepository
                .Setup(x => x.Update<VehicleMake>(vehicleMakeInstance))
                .Returns(true);
            var updatedVehicleMake = await testMockObject.VehicleMakeRepository.UpdateVehicleMake(vehicleMakeInstance);

            Assert.True(updatedVehicleMake);
        }

        [Fact]
        public async Task DeletedVehicleMake_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleMakeId = Guid.NewGuid();
            var fetchedVehicleMake = new VehicleMake { Id = vehicleMakeId, Abrv = "Abrv", Name = "Name" };

            testMockObject.MockGenericRepository
                .Setup(x => x.Get<VehicleMake>(vehicleMakeId))
                .ReturnsAsync(fetchedVehicleMake);
            testMockObject.MockGenericRepository
                .Setup(x => x.Delete<VehicleMake>(fetchedVehicleMake))
                .Returns(true);

            var deletedVehicleMake = await testMockObject.VehicleMakeRepository.DeleteVehicleMake(vehicleMakeId);

            Assert.True(deletedVehicleMake);
        }

    }
    public class TestMockObject
    {
        public TestMockObject()
        {
            MockGenericRepository = new Mock<IGenericRepository>();
            VehicleMakeRepository = new VehicleMakeRepository(MockGenericRepository.Object);
        }
        public IVehicleMakeRepository VehicleMakeRepository { get; }
        public Mock<IGenericRepository> MockGenericRepository { get; }
    }
}
