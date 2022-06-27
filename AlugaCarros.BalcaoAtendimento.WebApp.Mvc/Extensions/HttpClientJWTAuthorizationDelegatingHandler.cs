using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using System.Net.Http.Headers;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Extensions;
public class HttpClientJWTAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IAspNetUser _user;

    public HttpClientJWTAuthorizationDelegatingHandler(IAspNetUser user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.GetHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
        }

        var token = _user.GetUserToken();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}

public class HttpClientAgenciesAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IIdentityService _authenticationService;
    private readonly IConfiguration _configuration;
    private readonly IAspNetUser _user;

    public HttpClientAgenciesAuthorizationDelegatingHandler(IIdentityService authenticationService, IConfiguration configuration, IAspNetUser user)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;
        _user = user;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.GetHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
        }

        var login = await _authenticationService.Login(new UserLogin()
        {
            Email = _configuration["LoginAgencies"],
            Password = _configuration["PasswordAgencies"],
        });

        var token = login.Data.AccessToken;

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

