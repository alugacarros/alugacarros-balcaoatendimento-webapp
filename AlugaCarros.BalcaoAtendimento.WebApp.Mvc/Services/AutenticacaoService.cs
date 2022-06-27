using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Configuration.Exceptions;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using AlugaCarros.Core.Dtos;
using AlugaCarros.Core.WebApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services;
public class AuthenticationService : IIdentityService
{
    private readonly HttpClient _httpClient;

    private readonly IAspNetUser _user;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationService(IHttpClientFactory httpClientFactory,
                               IAspNetUser user,
                               IAuthenticationService authenticationService)
    {
        _httpClient = httpClientFactory.CreateClient("BalcaoAtendimentoBff");
        _user = user;
        _authenticationService = authenticationService;
    }

    public async Task<ResultDto<UserLoginResponse>> Login(UserLogin userLogin)
    {
        var loginContent = userLogin.ToStringContent();

        var response = await _httpClient.PostAsync("/api/v1/users/login", loginContent);

        if (!response.IsSuccessStatusCode) return ResultDto<UserLoginResponse>.Failed("Ocorreu um erro ao realizar Login!");

        var userLoginResponse = await response.Deserialize<UserLoginResponse>();

        userLoginResponse.SelectedAgency = userLogin.AgencyId;

        return ResultDto<UserLoginResponse>.Success(userLoginResponse, "");
    }

    public async Task RealizarLogin(UserLoginResponse resposta)
    {
        var token = ObterTokenFormatado(resposta.AccessToken);

        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", resposta.AccessToken));
        claims.Add(new Claim("agency", resposta.SelectedAgency));
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
            IsPersistent = true
        };

        await _authenticationService.SignInAsync(
            _user.GetHttpContext(),
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public async Task Logout()
    {
        await _authenticationService.SignOutAsync(
            _user.GetHttpContext(),
            CookieAuthenticationDefaults.AuthenticationScheme,
            null);
    }

    public static JwtSecurityToken ObterTokenFormatado(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }

    private bool HasResponseErrors(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);

            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}