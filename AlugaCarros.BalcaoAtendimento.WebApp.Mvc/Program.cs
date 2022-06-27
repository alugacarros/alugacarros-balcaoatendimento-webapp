using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Configuration;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Middleware;
using AlugaCarros.Core.ApiConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.AddSerilog("AlugaCarros.BalcaoAtendimento.WebApp");
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAppConfiguration();

app.Run();
