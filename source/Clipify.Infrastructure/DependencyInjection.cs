using Clipify.Application;
using Clipify.Application.Auth;
using Clipify.Application.Auth.Requests;
using Clipify.Application.WeatherForecasts.Requests;
using Clipify.Infrastructure.Database;
using Clipify.Infrastructure.SpotifyAuth;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Models;
using Clipify.Infrastructure.WeatherForecasts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clipify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SpotifyAuthSettings>(configuration.GetSection("SpotifyAuth"));

            services.AddHttpClient<SpotifyAuthClient>();

            services.AddSingleton<IDbContext, DbContext>();
            services.AddSingleton<IAuthCodeProvider, SpotifyAuthCodeProvider>();

            services.AddTransient<IAuthService, SpotifyAuthService>();
            services.AddTransient<IAuthUriBuilder, SpotifyAuthUriBuilder>();
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
    }
}