FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RockPaperScissors.WebAPI/RockPaperScissors.WebAPI.csproj", "RockPaperScissors.WebAPI/"]
COPY ["RockPaperScissors.Application/RockPaperScissors.Application.csproj", "RockPaperScissors.Application/"]
COPY ["RockPaperScissors.Domain/RockPaperScissors.Domain.csproj", "RockPaperScissors.Domain/"]
COPY ["RockPaperScissors.Infrastructure/RockPaperScissors.Infrastructure.csproj", "RockPaperScissors.Infrastructure/"]
COPY ["RockPaperScissors.Persistence/RockPaperScissors.Persistence.csproj", "RockPaperScissors.Persistence/"]
RUN dotnet restore "RockPaperScissors.WebAPI/RockPaperScissors.WebAPI.csproj"
COPY . .
WORKDIR "/src/RockPaperScissors.WebAPI"
RUN dotnet build "RockPaperScissors.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RockPaperScissors.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RockPaperScissors.WebAPI.dll"]
