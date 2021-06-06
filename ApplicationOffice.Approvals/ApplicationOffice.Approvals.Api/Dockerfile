FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /source
COPY . ./
RUN dotnet publish ApplicationOffice.Approvals/ApplicationOffice.Approvals.Api/ApplicationOffice.Approvals.Api.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
COPY localhost.pfx /app/localhost.pfx
ENTRYPOINT ["dotnet", "ApplicationOffice.Approvals.Api.dll"]