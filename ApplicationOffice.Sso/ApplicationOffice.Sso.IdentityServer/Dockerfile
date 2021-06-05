FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /source
COPY . ./
RUN dotnet publish ApplicationOffice.Sso/ApplicationOffice.Sso.IdentityServer/ApplicationOffice.Sso.IdentityServer.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ApplicationOffice.Sso.IdentityServer.dll"]
