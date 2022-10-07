using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.MVC.Models;
using Project.Service;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService _service;

        public VehicleModelController( IVehicleModelService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(string filter,
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
            var vehicleModels = _service.GetAllVehicleModels(pagedResult.Query);
            pagedResult.Data = vehicleModels;

            return View(pagedResult);
        }


        [HttpGet]
        public async Task<IActionResult> GetVehicleModel( Guid id)
        {
            var vehicleModel = await _service.GetVehicleModelAsync(id);
            if (vehicleModel is null)
            {
                return StatusCode(204);
            }
            return View(vehicleModel);
        }

        [HttpGet]
        public IActionResult CreateVehicleModel(Guid id)
        {

            var vehicleModel = new VehicleModelDTO() { VehicleMakeId = id};
            return View(vehicleModel);
        }
        [HttpPost]

        public async Task<IActionResult> CreateVehicleModel( VehicleModelDTO model)
        {
            model.Id = Guid.Empty;
            var vehicleModel = await _service.CreateVehicleModelAsync(model);
            if (vehicleModel == false)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVehicleModel(Guid id)
        {
            var vehicleModel = await _service.GetVehicleModelAsync(id);

            if (vehicleModel is null)
            {
                return StatusCode(204);
            }
            return View(vehicleModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicleModel( VehicleModelDTO model)
        {
            var isUpdated = await _service.UpdateVehicleModelAsync(model);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVehicleModel( Guid id)
        {

            var isDeleted = await _service.DeleteVehicleModelAsync(id);
            if (!isDeleted )
            {
                return StatusCode(204);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
