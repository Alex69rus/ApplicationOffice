FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o output
FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build-env /app/output/wwwroot .
COPY ApplicationOffice.Web.UI/nginx.conf /etc/nginx/nginx.conf
COPY ApplicationOffice.Web.UI/localhost.crt /etc/ssl/certs/localhost.crt
COPY ApplicationOffice.Web.UI/localhost.key /etc/ssl/private/localhost.key
EXPOSE 80
EXPOSE 443