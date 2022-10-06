using Microsoft.AspNetCore.Mvc;
using Ninject;
using Project.MVC.Models;
using Project.Service;
using Project.Service.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Project.MVC.Controllers
{

    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _service;
        private readonly IVehicleModelService _modelService;

        public VehicleMakeController(IVehicleMakeService makeService,
            IVehicleModelService modelService)
        {
            _service = makeService;
            _modelService = modelService;
        }

        [HttpGet]
        public IActionResult Index( string filter,
            int page = 1,
            int pageSize = 10,
            SortOrder sortOrder = SortOrder.Ascending,
            SortBy sortBy = SortBy.Name)
        {
            if (string.IsNullOrWhiteSpace(filter)) 
            {
                filter = string.Empty;
            }
            var pagedResult = new PageResult<VehicleMakeDTO>(filter, page, pageSize, sortOrder, sortBy);
            var vehicleMakes = _service.GetAllVehicleMakes(pagedResult.Query);
            pagedResult.Data = vehicleMakes;
            return View(pagedResult);
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
        public async Task<IActionResult> CreateVehicleMake( VehicleMakeDTO make)
        {

            var vehicleMake = await _service.CreateVehicleMakeAsync(make);
            if (vehicleMake == false)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateVehicleMake()
        {

            var vehicleMake = new VehicleMakeDTO();
            return View(vehicleMake);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVehicleMake(Guid id)
        {
            var vehicleMake = await _service.GetVehicleMakeAsync(id);

            if (vehicleMake is null)
            {
                return StatusCode(204);
            }
            return View(vehicleMake);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateVehicleMake(VehicleMakeDTO make)
        {
            var isUpdated = await _service.UpdateVehicleMakeAsync(make);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
        

        [HttpGet]
        public async Task<IActionResult> DeleteVehicleMake( Guid id)
        {

            var isDeleted = await _service.DeleteVehicleMakeAsync(id);
            if (!isDeleted)
            {
                return StatusCode(204);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}