using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.WebAPI.Tests.VehicleModelControllerTests
{
    public class VehicleModelControllerTests
    {
        [Fact]
        public async Task GetAllVehicleModels_ValidRequest_200Status()
        {
            var mockData = new MockData();

            mockData.MockService
                 .Setup(x => x.GetAllVehicleModels(It.IsAny<QueryParameters>()))
                 .ReturnsAsync(new List<VehicleModelDTO>());

            var result = await mockData.Controller.GetAllVehicleModels();
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.NotNull(okObjectResult.Value);
        }
        [Fact]
        public async Task GetVehicleModel_ValidRequest_200Status()
        {
            var mockData = new MockData();
            var vehicleModel = new VehicleModelDTO() { Id = Guid.NewGuid() };

            mockData.MockService
                .Setup(x => x.GetVehicleModel(vehicleModel.Id))
                .ReturnsAsync(vehicleModel);

            var response = await mockData.Controller.GetVehicleModel(vehicleModel.Id);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(vehicleModel, okObjectResult.Value);
        }
        [Fact]
        public async Task GetVehicleModel_NotFound_204Status()
        {
            var mockData = new MockData();
            var vehicleModel = new VehicleModelDTO() { Id = Guid.NewGuid() };

            mockData.MockService
                .Setup(x => x.GetVehicleModel(It.IsAny<Guid>()))
                .ReturnsAsync((VehicleModelDTO)default);

            var response = await mockData.Controller.GetVehicleModel(default);
            var statusCode = response as StatusCodeResult;
            Assert.NotNull(statusCode);
            Assert.Equal(204, statusCode.StatusCode);
        }
        [Fact]
        public async Task CreateVehicleModel_ValidRequest_200Status()
        {
            var mockData = new MockData();
            var vehicleModel = new VehicleModelDTO();

            mockData.MockService
                .Setup(x => x.CreateVehicleModel(It.IsAny<VehicleModelDTO>()))
                .ReturnsAsync(true);

            var response = await mockData.Controller.CreateVehicleModel(vehicleModel);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True((bool)okObjectResult.Value);
        }
        [Fact]
        public async Task CreateVehicleModel_ValidRequest_BadRequest()
        {
            var mockData = new MockData();
            var vehicleModel = new VehicleModelDTO();

            mockData.MockService
                .Setup(x => x.CreateVehicleModel(It.IsAny<VehicleModelDTO>()))
                .ReturnsAsync(false);

            var response = await mockData.Controller.CreateVehicleModel(vehicleModel);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            Assert.NotNull(BadRequestObjectResult);
            Assert.False((bool)BadRequestObjectResult.Value);
        }

        [Fact]
        public async Task UpdateVehicleModel_ValidRequest_204Status()
        {
            var mockData = new MockData();
            var vehicleModel = new VehicleModelDTO();

            mockData.MockService
                .Setup(x => x.UpdateVehicleModel(It.IsAny<VehicleModelDTO>()))
                .ReturnsAsync(true);

            var response = await mockData.Controller.UpdateVehicleModel(vehicleModel);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True((bool)okObjectResult.Value);
        }
        [Fact]
        public async Task UpdateVehicleModel_ValidRequest_BadRequest()
        {
            var mockData = new MockData();
            var vehicleModel = new VehicleModelDTO();

            mockData.MockService
                .Setup(x => x.UpdateVehicleModel(It.IsAny<VehicleModelDTO>()))
                .ReturnsAsync(false);

            var response = await mockData.Controller.UpdateVehicleModel(vehicleModel);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            Assert.NotNull(BadRequestObjectResult);
            Assert.False((bool)BadRequestObjectResult.Value);
        }
        [Fact]
        public async Task DeleteVehicleModel_ValidRequest_204Status()
        {
            var mockData = new MockData();
            var vehicleModelId = Guid.NewGuid();

            mockData.MockService
                .Setup(x => x.DeleteVehicleModel(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var response = await mockData.Controller.DeleteVehicleModel(vehicleModelId);
            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True((bool)okObjectResult.Value);
        }
        [Fact]
        public async Task DeleteVehicleModel_ValidRequest_BadRequest()
        {
            var mockData = new MockData();
            var vehicleModelId = Guid.NewGuid();

            mockData.MockService
                .Setup(x => x.DeleteVehicleModel(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var response = await mockData.Controller.DeleteVehicleModel(vehicleModelId);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            Assert.NotNull(BadRequestObjectResult);
            Assert.False((bool)BadRequestObjectResult.Value);
        }
    }
    public class MockData
    {
        public MockData()
        {
            MockService = new Mock<IVehicleModelService>();
            Controller = new VehicleModelController(MockService.Object);
        }
        public VehicleModelController Controller { get; }
        public Mock<IVehicleModelService> MockService { get; }

    }
}
