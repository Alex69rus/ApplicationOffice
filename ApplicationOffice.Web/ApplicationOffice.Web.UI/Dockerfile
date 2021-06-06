FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app
COPY . ./
RUN dotnet publish ApplicationOffice.Web/ApplicationOffice.Web.UI/ApplicationOffice.Web.UI.csproj -c Release -o output
FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build-env /app/output/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
COPY localhost.crt /etc/ssl/certs/localhost.crt
COPY localhost.key /etc/ssl/private/localhost.key
EXPOSE 80
EXPOSE 443