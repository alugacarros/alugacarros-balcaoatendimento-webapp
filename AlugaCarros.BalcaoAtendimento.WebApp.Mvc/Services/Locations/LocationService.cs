using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Locations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using AlugaCarros.Core.Dtos;
using AlugaCarros.Core.WebApi;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Locations;
public class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;
    private readonly IAspNetUser _user;

    public LocationService(IHttpClientFactory httpClientFactory, IAspNetUser user)
    {
        _httpClient = httpClientFactory.CreateClient("BalcaoAtendimentoBff");
        _user = user;
    }

    public async Task<ResultDto> CreateLocation(string reservationCode, string vehiclePlate)
    {
        var content = new { reservationCode, vehiclePlate }.ToStringContent();

        var response = await _httpClient.PostAsync("api/v1/locations", content);

        if (!response.IsSuccessStatusCode) return ResultDto.Failed("Ocorreu um erro criar a Locação");

        return ResultDto.Success("");
    }

    public async Task<ResultDto<List<LocationResponse>>> GetLocations()
    {
        var agencyCode = _user.GetSelectedAgency();

        if (string.IsNullOrEmpty(agencyCode)) return ResultDto<List<LocationResponse>>.Failed("Agência selecionada é inválida");

        var response = await _httpClient.GetAsync($"api/v1/locations/{agencyCode}");

        if (!response.IsSuccessStatusCode) return ResultDto<List<LocationResponse>>.Failed("Ocorreu um erro obter as locações");

        var locations = await response.Deserialize<List<LocationResponse>>();

        return ResultDto<List<LocationResponse>>.Success(
            locations.OrderByDescending(o => o.Date).ToList(), "");
    }
}
