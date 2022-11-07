using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Common;
using Project.Service;
using Project.Service.Common;

namespace Project.WebAPI
{
    [Route("api/[controller]")]
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService service;

        public VehicleModelController( IVehicleModelService service)
        {
            this.service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVehicleModels(string filter,
            int page = 1,
            int pageSize = 10,
            SortOrder sortOrder = SortOrder.Ascending,
            SortBy sortBy = SortBy.Name)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                filter = string.Empty;
            }
            var pagedResult = new PageResult<VehicleModelDTO>(filter, page, pageSize, sortOrder, sortBy);
            var vehicleModels = await service.GetAllVehicleModels(pagedResult.Query);
            pagedResult.Data = vehicleModels;

            return Ok(pagedResult);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetVehicleModel( Guid id)
        {
            var vehicleModel = await service.GetVehicleModel(id);

            return Ok(vehicleModel);
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> CreateVehicleModel( VehicleModelDTO model)
        {
            
            var isCreated = await service.CreateVehicleModel(model);
            if (!isCreated)
            {
                return BadRequest(isCreated);
            }
            //for (int i = 0; i < 100; i++)
            //{
            //    var test = new VehicleModelDTO { Id = Guid.NewGuid(), Name = $"name_{i}", Abrv = $"abrv_{i}",VehicleMakeId = new Guid("F9A23729-71B0-43F2-A110-01AE3EFEBE40") };
            //    await service.CreateVehicleModel(test);
            //}
            return Ok(isCreated);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateVehicleModel( VehicleModelDTO model)
        {
            var isUpdated = await service.UpdateVehicleModel(model);
            if (!isUpdated)
            {
                return BadRequest(isUpdated);
            }
            return Ok(isUpdated);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteVehicleModel( Guid id)
        {

            var isDeleted = await service.DeleteVehicleModel(id);
            if (!isDeleted )
            {
                return BadRequest(isDeleted);
            }
            return Ok(isDeleted);
        }

    }
}
