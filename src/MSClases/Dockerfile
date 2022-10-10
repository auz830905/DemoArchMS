FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /ms_clases
EXPOSE 80 
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR ./ 

COPY src/MSClases/MSClases.csproj src/MSClases/

RUN dotnet restore src/MSClases/MSClases.csproj

COPY . . 

WORKDIR /src/MSClases
RUN dotnet publish "MSClases.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "MSClases.csproj" -c Release -o /app/publish

FROM base AS final 
WORKDIR /app 
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "MSClases.dll"]