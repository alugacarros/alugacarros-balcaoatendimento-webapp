using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;
using AlugaCarros.Core.Dtos;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
public interface IIdentityService
{
    Task<ResultDto<UserLoginResponse>> Login(UserLogin userLogin);
    Task RealizarLogin(UserLoginResponse resposta);
    Task Logout();
}