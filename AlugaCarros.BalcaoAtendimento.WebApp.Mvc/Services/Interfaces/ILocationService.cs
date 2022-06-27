using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Locations;
using AlugaCarros.Core.Dtos;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
public interface ILocationService
{
    Task<ResultDto> CreateLocation(string reservationCode, string vehiclePlate);
    Task<ResultDto<List<LocationResponse>>> GetLocations();
}
