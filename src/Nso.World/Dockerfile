# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish Nso.World.csproj -c Release -o /app
# ---------- runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet","Nso.World.dll"]
