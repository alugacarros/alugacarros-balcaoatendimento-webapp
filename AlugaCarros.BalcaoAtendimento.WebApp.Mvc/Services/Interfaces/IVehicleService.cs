using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Vehicles;
using AlugaCarros.Core.Dtos;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;

public interface IVehicleService
{
    Task<ResultDto<VehicleByAgencyModel>> GetVehiclesByAgencyCode();
}
