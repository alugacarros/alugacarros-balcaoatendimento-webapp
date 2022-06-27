using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Agencies;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using AlugaCarros.Core.Dtos;
using AlugaCarros.Core.WebApi;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Agencies;
public class AgencyService : IAgencyService
{
    private readonly HttpClient _httpClient;
    public AgencyService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BalcaoAtendimentoAgencyBff");
    }

    public async Task<ResultDto<AgencyResponse>> GetAgencies()
    {
        var response = await _httpClient.GetAsync("api/v1/agencies");

        if (!response.IsSuccessStatusCode) return ResultDto<AgencyResponse>.Failed("Ocorreu um erro ao buscar as agências");

        var vehicles = await response.Deserialize<AgencyResponse>();

        return ResultDto<AgencyResponse>.Success(vehicles);
    }
}
