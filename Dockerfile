# ---------- Stage 1: Build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY HotelManagement.csproj ./
RUN dotnet restore HotelManagement.csproj

# Copy all project files
COPY . ./

# Publish project
RUN dotnet publish HotelManagement.csproj -c Release -o /app

# ---------- Stage 2: Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "HotelManagement.dll"]
