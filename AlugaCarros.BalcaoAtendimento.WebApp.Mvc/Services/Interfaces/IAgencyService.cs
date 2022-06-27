using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Agencies;
using AlugaCarros.Core.Dtos;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;

public interface IAgencyService
{
    Task<ResultDto<AgencyResponse>> GetAgencies();
}