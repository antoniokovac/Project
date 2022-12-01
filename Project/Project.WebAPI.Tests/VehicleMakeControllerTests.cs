using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Common;
using Project.Model.DatabaseModels;
using Project.Service;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.WebAPI.Tests
{
    public class VehicleMakeControllerTests
    {
        [Fact]
        public async Task GetAllVehicleMakes_ValidRequest_200Status()
        {
            var mockData = new MockData();

            mockData.MockService
                 .Setup(x => x.GetAllVehicleMakes(It.IsAny<QueryParameters>()))
                 .ReturnsAsync(new List<VehicleMakeDTO>());

            var result = await mockData.Controller.GetAllVehicleMakes();
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.NotNull(okObjectResult.Value);
        }
        [Fact]
        public async Task GetVehicleMake_ValidRequest_200Status()
        {
            var mockData = new MockData();
            var vehicleMake = new VehicleMakeDTO() { Id = Guid.NewGuid()};

            mockData.MockService
                .Setup(x => x.GetVehicleMake(vehicleMake.Id))
                .ReturnsAsync(vehicleMake);

            var response = await mockData.Controller.GetVehicleMake(vehicleMake.Id);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(vehicleMake, okObjectResult.Value);
        }
        [Fact]
        public async Task GetVehicleMake_NotFound_204Status()
        {
            var mockData = new MockData();
            var vehicleMake = new VehicleMakeDTO() { Id = Guid.NewGuid() };

            mockData.MockService
                .Setup(x => x.GetVehicleMake(It.IsAny<Guid>()))
                .ReturnsAsync((VehicleMakeDTO)default);

            var response = await mockData.Controller.GetVehicleMake(default);
            var statusCode = response as StatusCodeResult;
            Assert.NotNull(statusCode);
            Assert.Equal(204,statusCode.StatusCode) ;
        }
        [Fact]
        public async Task CreateVehicleMake_ValidRequest_200Status()
        {
            var mockData = new MockData();
            var vehicleMake = new VehicleMakeDTO();

            mockData.MockService
                .Setup(x => x.CreateVehicleMake(It.IsAny<VehicleMakeDTO>()))
                .ReturnsAsync(true);

            var response = await mockData.Controller.CreateVehicleMake(vehicleMake);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True((bool) okObjectResult.Value);
        }
        [Fact]
        public async Task CreateVehicleMake_ValidRequest_BadRequest()
        {
            var mockData = new MockData();
            var vehicleMake = new VehicleMakeDTO();

            mockData.MockService
                .Setup(x => x.CreateVehicleMake(It.IsAny<VehicleMakeDTO>()))
                .ReturnsAsync(false);

            var response = await mockData.Controller.CreateVehicleMake(vehicleMake);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            Assert.NotNull(BadRequestObjectResult);
            Assert.False((bool)BadRequestObjectResult.Value);
        }

        [Fact]
        public async Task UpdateVehicleMake_ValidRequest_204Status()
        {
            var mockData = new MockData();
            var vehicleMake = new VehicleMakeDTO();

            mockData.MockService
                .Setup(x => x.UpdateVehicleMake(It.IsAny<VehicleMakeDTO>()))
                .ReturnsAsync(true);

            var response = await mockData.Controller.UpdateVehicleMake(vehicleMake);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True((bool)okObjectResult.Value);
        }
        [Fact]
        public async Task UpdateVehicleMake_ValidRequest_BadRequest()
        {
            var mockData = new MockData();
            var vehicleMake = new VehicleMakeDTO();

            mockData.MockService
                .Setup(x => x.UpdateVehicleMake(It.IsAny<VehicleMakeDTO>()))
                .ReturnsAsync(false);

            var response = await mockData.Controller.UpdateVehicleMake(vehicleMake);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            Assert.NotNull(BadRequestObjectResult);
            Assert.False((bool)BadRequestObjectResult.Value);
        }
        [Fact]
        public async Task DeleteVehicleMake_ValidRequest_204Status()
        {
            var mockData = new MockData();
            var vehicleMakeId =  Guid.NewGuid();

            mockData.MockService
                .Setup(x => x.DeleteVehicleMake(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var response = await mockData.Controller.DeleteVehicleMake(vehicleMakeId);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True((bool)okObjectResult.Value);
        }
        [Fact]
        public async Task DeleteVehicleMake_ValidRequest_BadRequest()
        {
            var mockData = new MockData();
            var vehicleMakeId = Guid.NewGuid();

            mockData.MockService
                .Setup(x => x.DeleteVehicleMake(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var response = await mockData.Controller.DeleteVehicleMake(vehicleMakeId);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            Assert.NotNull(BadRequestObjectResult);
            Assert.False((bool)BadRequestObjectResult.Value);
        }
    }
    public class MockData
    {
        public MockData()
        {
            MockService = new Mock<IVehicleMakeService>();
            Controller = new VehicleMakeController(MockService.Object);
        }
        public VehicleMakeController Controller { get;}
        public Mock<IVehicleMakeService> MockService { get;}

    }
}
