# Use the official .NET 7 SDK image as a build stage.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the .csproj file and restore any dependencies (if needed).
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code.
COPY . ./

# Build the application for release.
RUN dotnet publish -c Release -o out

# Use the official .NET 7 runtime image for the final image.
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Set the entry point for the application.
ENTRYPOINT ["dotnet", "bitcoin-wif-derive.dll"]
