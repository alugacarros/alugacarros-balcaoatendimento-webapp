using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Extensions;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Middleware;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Agencies;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Interfaces;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Locations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Reservations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Users;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Services.Vehicles;
using AlugaCarros.Core.ApiConfiguration;
using AlugaCarros.Core.Middlewares;
using AlugaCarros.Core.WebApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Configuration;
public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddLocalization();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(options =>
                        {
                            options.LoginPath = "/login";
                            options.AccessDeniedPath = "/erro/403";
                        });

        services.AddDefaultApiConfiguration(configuration);

        services.RegistryDependencies(configuration);

        return services;
    }

    private static IServiceCollection RegistryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<HttpClientJWTAuthorizationDelegatingHandler>();
        services.AddTransient<HttpClientAgenciesAuthorizationDelegatingHandler>();
        services.AddHttpClient<HttpClientJWTAuthorizationDelegatingHandler>("BalcaoAtendimentoBff", configuration["BalcaoAtendimentoBffUrl"]);
        services.AddHttpClient<HttpClientAgenciesAuthorizationDelegatingHandler>("BalcaoAtendimentoAgencyBff", configuration["BalcaoAtendimentoBffUrl"]);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, AuthenticationService>();
        services.AddTransient<IAspNetUser, AspNetUser>();
        services.AddTransient<IReservationsService, ReservationsService>();
        services.AddTransient<IVehicleService, VehiclesService>();
        services.AddTransient<IAgencyService, AgencyService>();
        services.AddTransient<ILocationService, LocationService>();
        return services;
    }

    public static WebApplication UseAppConfiguration(this WebApplication app)
    {        
        app.UseExceptionHandler("/erro/500");
        app.UseStatusCodePagesWithRedirects("/erro/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<LoggingRequestMiddleware>();

        var supportedCulture = new CultureInfo("pt-BR");

        var supportedCultures = new List<CultureInfo>() { supportedCulture };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}