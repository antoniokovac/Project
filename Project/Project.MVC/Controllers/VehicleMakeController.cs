using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using Project.Service;
using Project.Service.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly ILogger<VehicleMakeController> _logger;
        private readonly IVehicleMakeService _service;

        public VehicleMakeController(ILogger<VehicleMakeController> logger, IVehicleMakeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(/*[FromBody] QueryParameters query*/)
        {
            var query = new QueryParameters();
            var vehicleMakes = _service.GetAllVehicleMakes(query);
            return View(vehicleMakes);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleMake([FromQuery] Guid id)
        {
            var vehicleMake = await _service.GetVehicleMakeAsync(id);
            if (vehicleMake is null)
            {
                return StatusCode(204);
            }
            return View(vehicleMake);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleMake([FromBody] VehicleMakeDTO make)
        {

            var vehicleMake = await _service.CreateVehicleMakeAsync(make);
            if (vehicleMake == false)
            {
                return BadRequest();
            }
            return View(vehicleMake);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicleMake([FromBody] VehicleMakeDTO make)
        {
            var isUpdated = await _service.UpdateVehicleMakeAsync(make);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return View(isUpdated);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleMake([FromQuery] Guid id)
        {

            var isDeleted = await _service.DeleteVehicleMakeAsync(id);
            if (!isDeleted == false)
            {
                return StatusCode(204);
            }
            return View(isDeleted);
        }

    }
}