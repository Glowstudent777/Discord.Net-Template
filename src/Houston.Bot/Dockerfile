FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY Houston.Bot/Houston.Bot.csproj Houston.Bot/
COPY Houston.Database/Houston.Database.csproj Houston.Database/
RUN dotnet restore Houston.Bot/Houston.Bot.csproj
COPY . .
WORKDIR /src/Houston.Bot
RUN dotnet build Houston.Bot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Houston.Bot.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Houston.Bot.dll"]