using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Reservations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using AlugaCarros.Core.Dtos;
using AlugaCarros.Core.WebApi;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Reservations;

public class ReservationsService : IReservationsService
{
    private readonly HttpClient _httpClient;
    private readonly IAspNetUser _user;

    public ReservationsService(IHttpClientFactory httpClientFactory,
                               IAspNetUser user)
    {
        _httpClient = httpClientFactory.CreateClient("BalcaoAtendimentoBff");
        _user = user;
    }

    public async Task<ResultDto<ReservationResponse>> GetReservationsByAgencyCode()
    {
        var agencyCode = _user.GetSelectedAgency();

        if (string.IsNullOrEmpty(agencyCode)) return ResultDto<ReservationResponse>.Failed("Agência selecionada é inválida");

        _httpClient.DefaultRequestHeaders.Add("agencyCode", agencyCode);

        var response = await _httpClient.GetAsync("api/v1/reservations");

        if (!response.IsSuccessStatusCode) return ResultDto<ReservationResponse>.Failed("Ocorreu um erro ao buscar os veículos");

        var reservations = await response.Deserialize<ReservationResponse>();

        return ResultDto<ReservationResponse>.Success(reservations, "");
    }

    public async Task<ResultDto<ReservationResponse>> GetReservationsOpenedByAgencyCode()
    {
        var agencyCode = _user.GetSelectedAgency();

        if (string.IsNullOrEmpty(agencyCode)) return ResultDto<ReservationResponse>.Failed("Agência selecionada é inválida");

        _httpClient.DefaultRequestHeaders.Add("agencyCode", agencyCode);

        var response = await _httpClient.GetAsync("api/v1/reservations/opened");

        if (!response.IsSuccessStatusCode) return ResultDto<ReservationResponse>.Failed("Ocorreu um erro ao buscar os veículos");

        var reservations = await response.Deserialize<ReservationResponse>();

        return ResultDto<ReservationResponse>.Success(reservations, "");
    }

    public async Task<ResultDto<ReservationResponseItem>> GetReservationByCode(string reservationCode)
    {
        var response = await _httpClient.GetAsync($"api/v1/reservations/{reservationCode}");

        if (!response.IsSuccessStatusCode) return ResultDto<ReservationResponseItem>.Failed("Ocorreu um erro ao buscar a reserva");

        var reservation = await response.Deserialize<ReservationResponseItem>();

        return ResultDto<ReservationResponseItem>.Success(reservation, "");
    }
}
