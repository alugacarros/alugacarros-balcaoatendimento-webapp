using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Reservations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Controllers
{
    [Route("reservas")]
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly IReservationsService _reservationsService;

        public ReservasController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet("PorData")]
        public async Task<IActionResult> PorData([FromQuery] DateTime? date = null)
        {
            date ??= DateTime.Now;
            var response = await _reservationsService.GetReservationsByAgencyCode();

            var filteredItems = date is null ? response.Data : response.Data.Where(w => w.PickupDate.Date == date.Value.Date).ToList();

            var model = new ReservationModel()
            {
                Items = filteredItems,
                Date = date.Value,
            };
            return View(model);
        }
        [HttpGet("EmAberto")]
        public async Task<IActionResult> EmAberto()
        {
            var response = await _reservationsService.GetReservationsOpenedByAgencyCode();

            var model = new ReservationModel()
            {
                Items = response.Data
            };
            return View(model);
        }
    }
}
