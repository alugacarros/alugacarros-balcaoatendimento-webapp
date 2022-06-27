using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Vehicles;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Controllers;

[Route("veiculos")]
[Authorize]
public class VeiculosController : Controller
{
    private readonly IVehicleService _vehicleService;

    public VeiculosController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int status = 0)
    {
        var vehicles = await _vehicleService.GetVehiclesByAgencyCode();

        if (vehicles.Fail) return NotFound();

        if (status != 0)
        {
            var filteredVehicles = new VehicleByAgencyModel();
            filteredVehicles.AddRange(vehicles.Data.Where(w => (int)w.Status == status).ToList());
            vehicles.Data = filteredVehicles;
            vehicles.Data.StatusFiltered = status;
        }

        return View(vehicles.Data);
    }
}