FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Presentation/ETicaret.API/ETicaret.API.csproj", "Presentation/ETicaret.API/"]
RUN dotnet restore "Presentation/ETicaret.API/ETicaret.API.csproj"
COPY . .
WORKDIR "/src/Presentation/ETicaret.API"
RUN dotnet build "ETicaret.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETicaret.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETicaret.API.dll"]
