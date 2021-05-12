﻿using Clipify.Application.Auth.Requests;
using Clipify.Application.WeatherForecasts.Requests;
using Clipify.Infrastructure.SpotifyAuth;
using Clipify.Infrastructure.WeatherForecasts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clipify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthService, SpotifyAuthService>();
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            services.Configure<SpotifyAuthService.Settings>(configuration.GetSection("SpotifyAuth"));

            return services;
        }
    }
}