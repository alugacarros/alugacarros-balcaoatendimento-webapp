using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Reservations;
using AlugaCarros.Core.Dtos;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;

public interface IReservationsService
{
    Task<ResultDto<ReservationResponse>> GetReservationsByAgencyCode();
    Task<ResultDto<ReservationResponse>> GetReservationsOpenedByAgencyCode();
    Task<ResultDto<ReservationResponseItem>> GetReservationByCode(string reservationCode);
}