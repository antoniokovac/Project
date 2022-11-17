using Project.Common;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Common;
using Moq;
using AutoMapper;

namespace Project.Service.Tests
{
    public class VehicleMakeServiceTests
    {
        [Fact]
        public async Task GetAllVehicleMakes_ValidRequest_ReturnsList()
        {
            var testMockObject = new TestMockObject();
            var databaseResponse = new List<VehicleMake>();
            var query = new QueryParameters();
            testMockObject.MockVehicleMakeRepository
                .Setup(x => x.GetAllVehicleMakes(query))
                .ReturnsAsync(databaseResponse);
            testMockObject.MockMapper
                .Setup(x => x.Map<List<VehicleMakeDTO>>(databaseResponse))
                .Returns(new List<VehicleMakeDTO>());

            var allVehicleMakes = await testMockObject.VehicleMakeService.GetAllVehicleMakes(query);

            Assert.NotNull(allVehicleMakes);
        }

        [Fact]
        public async Task GetVehicleMake_ValidRequest_ReturnsVehicleMakeDTO()
        {
            var testMockObject = new TestMockObject();
            var vehicleMakeId = Guid.NewGuid();
            var databaseResponse = new VehicleMake { Id = vehicleMakeId };

            testMockObject.MockVehicleMakeRepository
                .Setup(x => x.GetVehicleMake(vehicleMakeId))
                .ReturnsAsync(databaseResponse);

            testMockObject.MockMapper
                .Setup(x => x.Map<VehicleMakeDTO>(databaseResponse))
                .Returns(new VehicleMakeDTO {Id = vehicleMakeId });

            var vehicleMake = await testMockObject.VehicleMakeService.GetVehicleMake(vehicleMakeId);

            Assert.NotNull(vehicleMake);
            Assert.Equal(vehicleMakeId, vehicleMake.Id);
        }
        [Fact]
        public async Task CreateVehicleMake_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleMakeDTOInstance = new VehicleMakeDTO();
            var vehicleMakeInstance = new VehicleMake();

            testMockObject.MockVehicleMakeRepository
                .Setup(x => x.CreateVehicleMake(vehicleMakeInstance))
                .ReturnsAsync(true);
            testMockObject.MockMapper
                .Setup(x => x.Map<VehicleMake>(vehicleMakeDTOInstance))
                .Returns((VehicleMake input) => new VehicleMake { Id = input.Id });

            var createdVehicleMake = await testMockObject.VehicleMakeService.CreateVehicleMake(vehicleMakeDTOInstance);

            Assert.True(createdVehicleMake);
        }

        [Fact]
        public async Task UpdateVehicleMake_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();

            var vehicleMakeInstance = new VehicleMake { Id = Guid.NewGuid() };
            var vehicleMakeDTOInstance = new VehicleMakeDTO { Id = vehicleMakeInstance.Id };

            testMockObject.MockVehicleMakeRepository
                .Setup(x => x.UpdateVehicleMake(vehicleMakeInstance))
                .ReturnsAsync(true);

            testMockObject.MockMapper
                .Setup(x => x.Map<VehicleMakeDTO>(vehicleMakeInstance))
                .Returns(vehicleMakeDTOInstance);
            var updatedVehicleMake = await testMockObject.VehicleMakeService.UpdateVehicleMake(vehicleMakeDTOInstance);

            Assert.True(updatedVehicleMake);
        }

        [Fact]
        public void DeleteVehicleMake_ValidRequest_ReturnsTrue()
        {
            var testMockObject = new TestMockObject();
            var vehicleMakeId = Guid.NewGuid();
            var databaseResponse = new VehicleMake { Id = vehicleMakeId };

            testMockObject.MockVehicleMakeRepository
                .Setup(x => x.DeleteVehicleMake(vehicleMakeId))
                .ReturnsAsync(true);

            var vehicleMake = testMockObject.VehicleMakeService.DeleteVehicleMake(vehicleMakeId);

            Assert.True(vehicleMake.Result);
        }
    }
    public class TestMockObject
    {
        public TestMockObject()
        {
            MockVehicleMakeRepository = new Mock<IVehicleMakeRepository>();
            MockMapper = new Mock<IMapper>();
            VehicleMakeService = new VehicleMakeService(MockVehicleMakeRepository.Object, MockMapper.Object);
        }
        public IVehicleMakeService VehicleMakeService{ get; }
        public Mock<IVehicleMakeRepository> MockVehicleMakeRepository { get; }

        public Mock<IMapper> MockMapper { get; }
    }
}


