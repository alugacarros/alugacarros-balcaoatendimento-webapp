using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Vehicles;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using AlugaCarros.Core.Dtos;
using AlugaCarros.Core.WebApi;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Vehicles;
public class VehiclesService : IVehicleService
{
    private readonly HttpClient _httpClient;
    private readonly IAspNetUser _user;

    public VehiclesService(IHttpClientFactory httpClientFactory,
                               IAspNetUser user)
    {
        _httpClient = httpClientFactory.CreateClient("BalcaoAtendimentoBff");
        _user = user;
    }

    public async Task<ResultDto<VehicleByAgencyModel>> GetVehiclesByAgencyCode()
    {
        var agencyCode = _user.GetSelectedAgency();

        if (string.IsNullOrEmpty(agencyCode)) return ResultDto<VehicleByAgencyModel>.Failed("Agência selecionada é inválida");

        _httpClient.DefaultRequestHeaders.Add("agencyCode", agencyCode);

        var response = await _httpClient.GetAsync("api/v1/vehicles");

        if (!response.IsSuccessStatusCode) return ResultDto<VehicleByAgencyModel>.Failed("Ocorreu um erro ao buscar os veículos");

        var vehicles = await response.Deserialize<VehicleByAgencyModel>();

        return ResultDto<VehicleByAgencyModel>.Success(vehicles, "");
    }
}
