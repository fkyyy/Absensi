# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Salin file csproj dari setiap proyek untuk melakukan restore dependensi
COPY API/API.csproj .API/
COPY Application/Application.csproj .Application/
COPY Infrastructure/Infrastructure.csproj .Infrastructure/
COPY Domain/Domain.csproj .Domain/
RUN dotnet restore API/API.csproj


# Salin seluruh kode sumber
COPY . .  

# Build dan publish aplikasi
RUN dotnet publish API/API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Salin hasil build dari stage 1
COPY --from=build-env /app/publish ./

# Expose port agar bisa diakses dari luar
EXPOSE 8000

# Jalankan aplikasi
CMD ["dotnet", "API.dll"]