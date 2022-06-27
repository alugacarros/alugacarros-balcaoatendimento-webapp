FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR .
COPY ["AlugaCarros.BalcaoAtendimento.WebApp.Mvc/AlugaCarros.BalcaoAtendimento.WebApp.Mvc.csproj", "./AlugaCarros.BalcaoAtendimento.WebApp.Mvc/"]

RUN dotnet restore "AlugaCarros.BalcaoAtendimento.WebApp.Mvc/AlugaCarros.BalcaoAtendimento.WebApp.Mvc.csproj"
COPY . .
WORKDIR "AlugaCarros.BalcaoAtendimento.WebApp.Mvc"
RUN dotnet build "AlugaCarros.BalcaoAtendimento.WebApp.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlugaCarros.BalcaoAtendimento.WebApp.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlugaCarros.BalcaoAtendimento.WebApp.Mvc.dll"]