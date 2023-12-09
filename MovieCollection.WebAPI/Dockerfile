#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieCollection.WebAPI/MovieCollection.WebAPI.csproj", "MovieCollection.WebAPI/"]
COPY ["MovieCollection.Core/MovieCollection.Core.csproj", "MovieCollection.Core/"]
COPY ["MovieCollection.Domain/MovieCollection.Movie/MovieCollection.Movie.Domain/MovieCollection.Movie.Domain.csproj", "MovieCollection.Domain/MovieCollection.Movie/MovieCollection.Movie.Domain/"]
COPY ["MovieCollection.Domain/MovieCollection.Movie/MovieCollection.Movie.Persistence/MovieCollection.Movie.Persistence.csproj", "MovieCollection.Domain/MovieCollection.Movie/MovieCollection.Movie.Persistence/"]
COPY ["MovieCollection.Domain/MovieCollection.UserAdministration/MovieCollection.UserAdministration.Domain/MovieCollection.UserAdministration.Domain.csproj", "MovieCollection.Domain/MovieCollection.UserAdministration/MovieCollection.UserAdministration.Domain/"]
COPY ["MovieCollection.Domain/MovieCollection.UserAdministration/MovieCollection.UserAdministration.Persist/MovieCollection.UserAdministration.Persistant.csproj", "MovieCollection.Domain/MovieCollection.UserAdministration/MovieCollection.UserAdministration.Persist/"]
RUN dotnet restore "MovieCollection.WebAPI/MovieCollection.WebAPI.csproj"
COPY . .
WORKDIR "/src/MovieCollection.WebAPI"
RUN dotnet build "MovieCollection.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieCollection.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieCollection.WebAPI.dll"]