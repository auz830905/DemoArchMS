FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /ms_profesores
EXPOSE 80 
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR ./ 

COPY src/MSProfesores/MSProfesores.csproj src/MSProfesores/

RUN dotnet restore src/MSProfesores/MSProfesores.csproj

COPY . . 

WORKDIR /src/MSProfesores
RUN dotnet publish "MSProfesores.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "MSProfesores.csproj" -c Release -o /app/publish

FROM base AS final 
WORKDIR /app 
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "MSProfesores.dll"]