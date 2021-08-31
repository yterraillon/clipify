<h1 align="center">Clipify</h1>
<p align="center">
Copy and backup your Spotify playlists
</p>
<p align="center">
    <a href="https://github.com/yterraillon/clipify/releases" target="_blank">
        <img src="https://img.shields.io/badge/version-v0.0.1-blue?style=for-the-badge&logo=none" alt="clipify version" />
    </a>
    <a href="https://github.com/yterraillon/clipify/actions/workflows/dotnet.yml" target="_blank">
    <img src="https://img.shields.io/github/workflow/status/yterraillon/clipify/.NET/main.svg?style=for-the-badge"
      alt="Build Status" />
    </a>
    <a href="https://github.com/yterraillon/clipify/commits/main">
        <img src="https://img.shields.io/github/last-commit/yterraillon/clipify.svg?style=for-the-badge&logo=github&logoColor=white" alt="GitHub last commit">
    </a>
    <img src="https://img.shields.io/badge/license-mit-red?style=for-the-badge&logo=none" alt="license" />
</p>
<br>

## Table of Content
- [Architecture](#architecture)
- [Technologies](#technologies)
- [CI / CD](#ci)
- [Quick Start](#quick-start)
- [Open Issue](#open-an-issue)
- [Design](#design)
- [License](#license)


<br>

## Architecture

Hexagonal Architecture based on [ASP.NET Core Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture).
<br>
Spotify Authentication set up with [Authorization Code Flow with Proof Key for Code Exchange (PKCE)](https://developer.spotify.com/documentation/general/guides/authorization-guide/)

<br>

## Technologies

- .NET Core 5.0 (C# 9.0)
- Blazor Server App
- MediatR · AutoMapper · FluentValidation · LiteDB

<br>

## CI

[GitHub Actions](https://github.com/yterraillon/clipify/actions/workflows/dotnet.yml) · [Docker Hub](https://hub.docker.com/r/clipify/clipifyweb)

<br>

## Quick Start

### ⚡️ Local

#### Using Visual Studio 2019

First of all, [download the latest release](https://github.com/yterraillon/clipify/releases) of Clipify.

- Open `clipify.sln` with Visual Studio 2019+.
- Select Debug / Release configuration
- Run using `IIS Express`

<br>

#### Using .NET Core CLI

[Download .NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) from Microsoft.

```sh
dotnet restore "Clipify.Web.csproj"
dotnet build "Clipify.Web.csproj" -c Release -o "/build"
dotnet publish "Clipify.Web.csproj" -c Release -o "/build"

cd build/
dotnet Clipify.Web.dll
```

<br>

-----

<br>

### 🐳 Docker

If you don't want to install Clipify on your system, feel free to use our official Docker image and run Clipify from isolated container:

```sh
docker run --name=clipify clipify/clipifyweb:latest
```

<br>

## Open an Issue

If you want to report a bug or request a new feature, you can [open a new issue](https://github.com/yterraillon/clipify/issues/new/choose)

<br>

## Design

Color palette : https://coolors.co/e2edb0-84dc9e-49cf6b-31ba6e-2f4b98
<br>
Icons : https://www.flaticon.com/packs/music-224

<br>

## License

Distributed under the [MIT License](https://github.com/yterraillon/clipify/blob/main/LICENSE). See `LICENSE` for more information.
<br>
Icons made by [Pixel Perfect](https://www.flaticon.com/authors/pixel-perfect) from [flaticon](https://www.flaticon.com/) 
