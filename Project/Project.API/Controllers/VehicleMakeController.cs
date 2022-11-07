using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Service;
using Project.Service.Common;
using System.Diagnostics;
using System.Text.Json;

namespace Project.WebAPI
{
    [Route("api/[controller]")]
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService service;

        public VehicleMakeController(IVehicleMakeService makeService)
        {
            service = makeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVehicleMakes( string filter,
            int page = 1,
            int pageSize = 10,
            SortOrder sortOrder = SortOrder.Ascending,
            SortBy sortBy = SortBy.Name)
        {
            if (string.IsNullOrWhiteSpace(filter))             {
                filter = string.Empty;
            }
            //for(int i = 0; i <100; i++)
            //{
            //    var test = new VehicleMakeDTO { Id = Guid.NewGuid(), Name = $"Name_{i}", Abrv = $"Abrv_{i}" };
            //    await service.CreateVehicleMake(test);
            //}
            var pagedResult = new PageResult<VehicleMakeDTO>(filter, page, pageSize, sortOrder, sortBy);
            var vehicleMakes = await service.GetAllVehicleMakes(pagedResult.Query);
            pagedResult.Data = vehicleMakes;
            return  Ok( pagedResult);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetVehicleMake([FromQuery] Guid id)
        {
            var vehicleMake = await service.GetVehicleMake(id);

            return Ok(vehicleMake);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateVehicleMake( VehicleMakeDTO make)
        {

            var isCreated = await service.CreateVehicleMake(make);
            if (!isCreated)
            {
                return BadRequest(isCreated);
            }
            return Ok(isCreated);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateVehicleMake(VehicleMakeDTO make)
        {
            var isUpdated = await service.UpdateVehicleMake(make);
            if (!isUpdated)
            {
                return BadRequest(isUpdated);
            }
            return Ok(isUpdated);
        }
        

        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteVehicleMake( Guid id)
        {

            var isDeleted = await service.DeleteVehicleMake(id);
            if (!isDeleted)
            {
                return BadRequest(isDeleted);
            }
            return Ok(isDeleted);
        }

    }
}