# Use .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaskManagerSystem.API/TaskManagerSystem.API.csproj", "TaskManagerSystem.API/"]
RUN dotnet restore "TaskManagerSystem.API/TaskManagerSystem.API.csproj"
COPY . .
WORKDIR "/src/TaskManagerSystem.API"
RUN dotnet publish "TaskManagerSystem.API.csproj" -c Release -o /app/publish

# Use .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskManagerSystem.API.dll"]
