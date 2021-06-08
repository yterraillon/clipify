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
- [License](#license)


## Architecture

Hexagonal Architecture based on [ASP.NET Core Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture).


## Technologies

- C# 8.0
- Blazor Server App
- MediatR · AutoMapper · FluentValidation · LiteDB


## CI

[GitHub Actions](https://github.com/yterraillon/clipify/actions/workflows/dotnet.yml) · [Docker Hub](https://hub.docker.com/r/clipify/clipifyweb)



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

```
dotnet restore "Clipify.Web.csproj"
dotnet build "Clipify.Web.csproj" -c Release -o "/build"
dotnet publish "Clipify.Web.csproj" -c Release -o "/build"

cd build/
dotnet Clipify.Web.dll
```

-----

### 🐳 Docker

If you don't want to install Clipify on your system, feel free to use our official Docker image and run Clipify from isolated container:

```
docker run --name=clipify clipify/clipifyweb:latest
```


## Open an Issue

If you want to report a bug or request a new feature, you can [open a new issue](https://github.com/yterraillon/clipify/issues/new/choose)

## License

Distributed under the [MIT License](https://github.com/yterraillon/clipify/blob/main/LICENSE). See `LICENSE` for more information.
