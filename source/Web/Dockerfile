#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["source/Web/Web.csproj", "source/Web/"]
COPY ["source/Application/Application.csproj", "source/Application/"]
COPY ["source/Events/Events.csproj", "source/Events/"]
COPY ["source/Domain/Domain.csproj", "source/Domain/"]
COPY ["source/Infrastructure/Infrastructure.csproj", "source/Infrastructure/"]
RUN dotnet restore "source/Web/Web.csproj"
COPY . .
WORKDIR "/src/source/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]