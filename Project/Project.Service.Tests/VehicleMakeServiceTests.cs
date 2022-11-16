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
        //[Fact]
        //public async Task CreateVehicleMake_ValidRequest_ReturnsTrue()
        //{
        //    var testMockObject = new TestMockObject();

        //    var vehicleMakeInstance = new VehicleMake { Id = Guid.NewGuid() };

        //    testMockObject.MockGenericRepository
        //        .Setup(x => x.Create<VehicleMake>(vehicleMakeInstance))
        //        .ReturnsAsync(true);
        //    var createdVehicleMake = await testMockObject.VehicleMakeRepository.CreateVehicleMake(vehicleMakeInstance);

        //    Assert.True(createdVehicleMake);
        //}

        //[Fact]
        //public async Task UpdateVehicleMake_ValidRequest_ReturnsTrue()
        //{
        //    var testMockObject = new TestMockObject();

        //    var vehicleMakeInstance = new VehicleMake { Id = Guid.NewGuid() };

        //    testMockObject.MockGenericRepository
        //        .Setup(x => x.Update<VehicleMake>(vehicleMakeInstance))
        //        .Returns(true);
        //    var updatedVehicleMake = await testMockObject.VehicleMakeRepository.UpdateVehicleMake(vehicleMakeInstance);

        //    Assert.True(updatedVehicleMake);
        //}

        //[Fact]
        //public async Task DeletedVehicleMake_ValidRequest_ReturnsTrue()
        //{
        //    var testMockObject = new TestMockObject();

        //    var vehicleMakeId = Guid.NewGuid();
        //    var fetchedVehicleMake = new VehicleMake { Id = vehicleMakeId, Abrv = "Abrv", Name = "Name" };

        //    testMockObject.MockGenericRepository
        //        .Setup(x => x.Get<VehicleMake>(vehicleMakeId))
        //        .ReturnsAsync(fetchedVehicleMake);
        //    testMockObject.MockGenericRepository
        //        .Setup(x => x.Delete<VehicleMake>(fetchedVehicleMake))
        //        .Returns(true);

        //    var deletedVehicleMake = await testMockObject.VehicleMakeRepository.DeleteVehicleMake(vehicleMakeId);

        //    Assert.True(deletedVehicleMake);
        //}

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


