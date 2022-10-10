FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR /app

COPY src/Shared/Shared.csproj src/Shared/
COPY src/WebApp/WebApp.csproj src/WebApp/

RUN dotnet restore src/WebApp/WebApp.csproj

COPY . .
RUN dotnet publish -c Release -o output

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/output/wwwroot .