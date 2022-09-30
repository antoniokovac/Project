using Microsoft.AspNetCore.Mvc;
using Project.Service;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly ILogger<VehicleModelController> _logger;
        private readonly IVehicleModelService _service;

        public VehicleModelController(ILogger<VehicleModelController> logger, IVehicleModelService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpPost]
        public IActionResult Index([FromBody] QueryParameters query)
        {
            var vehicleModels = _service.GetAllVehicleModels(query);
            return View(vehicleModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleModel([FromQuery] Guid id)
        {
            var vehicleModel = await _service.GetVehicleModelAsync(id);
            if (vehicleModel is null)
            {
                return StatusCode(204);
            }
            return View(vehicleModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleModel([FromBody] VehicleModelDTO Model)
        {

            var vehicleModel = await _service.CreateVehicleModelAsync(Model);
            if (vehicleModel == false)
            {
                return BadRequest();
            }
            return View(vehicleModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicleModel([FromBody] VehicleModelDTO Model)
        {
            var isUpdated = await _service.UpdateVehicleModelAsync(Model);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return View(isUpdated);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleModel([FromQuery] Guid id)
        {

            var isDeleted = await _service.DeleteVehicleModelAsync(id);
            if (!isDeleted == false)
            {
                return StatusCode(204);
            }
            return View(isDeleted);
        }

    }
}
