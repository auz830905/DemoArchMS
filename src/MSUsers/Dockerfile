FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /ms_users
EXPOSE 80 
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR ./ 

COPY src/Auth/Auth.csproj src/Auth/
COPY src/MSUsers/MSUsers.csproj src/MSUsers/

RUN dotnet restore src/MSUsers/MSUsers.csproj

COPY . . 

WORKDIR /src/MSUsers
RUN dotnet publish "MSUsers.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "MSUsers.csproj" -c Release -o /app/publish

FROM base AS final 
WORKDIR /app 
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "MSUsers.dll"]