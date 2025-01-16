# Use the official .NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaskManagerSystem.API/TaskManagerSystem.API.csproj", "TaskManagerSystem.API/"]
RUN dotnet restore "TaskManagerSystem.API/TaskManagerSystem.API.csproj"
COPY . .
WORKDIR "/src/TaskManagerSystem.API"
RUN dotnet publish -c Release -o /app/publish

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskManagerSystem.API.dll"]
