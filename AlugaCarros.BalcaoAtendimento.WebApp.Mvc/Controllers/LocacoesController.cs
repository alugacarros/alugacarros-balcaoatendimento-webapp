using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Locations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Controllers;

[Route("locacoes")]
[Authorize]
public class LocacoesController : Controller
{
    private readonly IReservationsService _reservationsService;
    private readonly IVehicleService _vehicleService;
    private readonly ILocationService _locationService;
    public LocacoesController(IReservationsService reservationsService, IVehicleService vehicleService, ILocationService locationService)
    {
        _reservationsService = reservationsService;
        _vehicleService = vehicleService;
        _locationService = locationService;
    }

    [HttpGet("abrir")]
    public async Task<IActionResult> Abrir([FromQuery][Required] string reservationCode)
    {
        var reservationResponse = await _reservationsService.GetReservationByCode(reservationCode);
        var vehiclesResponse = await _vehicleService.GetVehiclesByAgencyCode();

        if (reservationResponse.Fail || vehiclesResponse.Fail)
        {
            var errors = new List<string>() { "Não é possível abrir uma locação para esta reserva!" };

            if (reservationResponse.Fail)
                errors.Add("Reserva não encontrada");
            if (vehiclesResponse.Fail)
                errors.Add("Veículo não encontrado");
            
            TempData["Erros"] = errors;

            return RedirectToAction("Nova", new NewLocationModel() { ReservationCode = reservationCode });
        }

        if (reservationResponse.Data.Status != Models.Reservations.ReservationStatus.Opened)
        {
            TempData["Erros"] = new List<string>() { "Não é possível abrir uma locação para esta reserva!" };

            return RedirectToAction("Nova", new NewLocationModel() { ReservationCode = reservationCode });
        }

        var model = new OpenLocationModel()
        {
            ReservationCode = reservationResponse.Data.ReservationCode,
            Reservation = reservationResponse.Data,
            Vehicles = vehiclesResponse.Data
        };
        return View(model);
    }

    [HttpPost("abrir")]
    public async Task<IActionResult> AbrirLocacao(OpenLocationModel openLocation)
    {
        if (!ModelState.IsValid)
        {
            var reservationResponse = await _reservationsService.GetReservationByCode(openLocation.ReservationCode);
            var vehiclesResponse = await _vehicleService.GetVehiclesByAgencyCode();

            if (reservationResponse.Fail || vehiclesResponse.Fail) return View(openLocation);

            var model = new OpenLocationModel()
            {
                ReservationCode = reservationResponse.Data.ReservationCode,
                Reservation = reservationResponse.Data,
                Vehicles = vehiclesResponse.Data
            };

            return View("Abrir", model);
        }

        var response = await _locationService.CreateLocation(
            openLocation.ReservationCode, openLocation.SelectedVehicle);

        if (response.Fail) return View(openLocation);

        return RedirectToAction("Index", "Locacoes");
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var locationsResponse = await _locationService.GetLocations();
        if (locationsResponse.Fail)
            return BadRequest();

        return View(locationsResponse.Data);

    }

    [HttpGet("nova")]
    public async Task<IActionResult> Nova()
    {
        return View(new NewLocationModel());
    }
}