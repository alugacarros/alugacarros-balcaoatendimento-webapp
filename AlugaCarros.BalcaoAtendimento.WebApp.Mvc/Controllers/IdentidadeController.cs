using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Controllers
{
    public class IdentidadeController : Controller
    {
        private readonly IIdentityService _authenticationService;
        private readonly IAgencyService _agencyService;
        public IdentidadeController(IIdentityService authenticationService, IAgencyService agencyService)
        {
            _authenticationService = authenticationService;
            _agencyService = agencyService;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            var agenciesResponse = await _agencyService.GetAgencies();

            if (agenciesResponse.Fail) return BadRequest();

            var loginModel = new UserLogin()
            {
                Agencies = agenciesResponse.Data
            };

            return View(loginModel);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            ModelState.Remove("Agencies");
            if (!ModelState.IsValid) return View(userLogin);

            var loginResponse = await _authenticationService.Login(userLogin);

            if (loginResponse.Fail)
            {
                TempData["Erros"] = new List<string>() { loginResponse.Message };
                return View();
            }

            await _authenticationService.RealizarLogin(loginResponse.Data);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {

            await _authenticationService.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
