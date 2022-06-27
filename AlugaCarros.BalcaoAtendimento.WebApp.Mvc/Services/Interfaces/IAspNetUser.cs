using System.Security.Claims;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetSelectedAgency();
        string GetUserToken();
        string GetUserRefreshToken();
        bool IsAuthenticated();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }
}