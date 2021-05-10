#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
VOLUME /config /data
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

# Clipify.Application
COPY ["source/Clipify.Application/Clipify.Application.csproj", "source/Clipify.Application/"]
RUN dotnet restore "source/Clipify.Application/Clipify.Application.csproj"
COPY . .
WORKDIR "/src/source/Clipify.Application"
RUN dotnet build "Clipify.Application.csproj" -c Release -o /app/build

# Clipify.Domain
COPY ["source/Clipify.Domain/Clipify.Domain.csproj", "source/Clipify.Domain/"]
RUN dotnet restore "source/Clipify.Domain/Clipify.Domain.csproj"
COPY . .
WORKDIR "/src/source/Clipify.Domain"
RUN dotnet build "Clipify.Domain.csproj" -c Release -o /app/build

# Clipify.Infrastructure
COPY ["source/Clipify.Infrastructure/Clipify.Infrastructure.csproj", "source/Clipify.Infrastructure/"]
RUN dotnet restore "source/Clipify.Infrastructure/Clipify.Infrastructure.csproj"
COPY . .
WORKDIR "/src/source/Clipify.Infrastructure"
RUN dotnet build "Clipify.Infrastructure.csproj" -c Release -o /app/build

# Clipify.Web
COPY ["source/Clipify.Web/Clipify.Web.csproj", "source/Clipify.Web/"]
RUN dotnet restore "source/Clipify.Web/Clipify.Web.csproj"
COPY . .
WORKDIR "/src/source/Clipify.Web"
RUN dotnet build "Clipify.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clipify.Application.csproj" -c Release -o /app/publish
RUN dotnet publish "Clipify.Domain.csproj" -c Release -o /app/publish
RUN dotnet publish "Clipify.Infrastructure.csproj" -c Release -o /app/publish
RUN dotnet publish "Clipify.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clipify.Web.dll"]