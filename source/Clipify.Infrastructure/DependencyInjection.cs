﻿using Clipify.Application.Auth.Requests;
using Clipify.Application.Database;
using Clipify.Application.Spotify.Clients;
using Clipify.Application.Users;
using Clipify.Application.WeatherForecasts.Requests;
using Clipify.Infrastructure.Database;
using Clipify.Infrastructure.Database.Repositories;
using Clipify.Infrastructure.Spotify.Clients;
using Clipify.Infrastructure.SpotifyAuth;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Models;
using Clipify.Infrastructure.WeatherForecasts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;

namespace Clipify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SpotifyAuthSettings>(configuration.GetSection("SpotifyAuth"));

            services.AddHttpClient<SpotifyAuthClient>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IDbContext, DbContext>();
            services.AddSingleton<ISpotifyClient, SpotifyClient>();
            services.AddSingleton<IAuthCodeProvider, SpotifyAuthCodeProvider>();

            // Repositories
            services.AddTransient<IRepository<User, string>, Repository<User, UserDto, string>>();

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IAuthService, SpotifyAuthService>();
            services.AddTransient<IAuthUriBuilder, SpotifyAuthUriBuilder>();
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
    }
}