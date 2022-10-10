FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /api_gateway
EXPOSE 80 
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR ./ 

COPY src/Shared/Shared.csproj src/Shared/
COPY src/APIGateway/APIGateway.csproj src/APIGateway/

RUN dotnet restore src/APIGateway/APIGateway.csproj

COPY . . 

WORKDIR /src/APIGateway
RUN dotnet publish "APIGateway.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "APIGateway.csproj" -c Release -o /app/publish

FROM base AS final 
WORKDIR /app 
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "APIGateway.dll"]