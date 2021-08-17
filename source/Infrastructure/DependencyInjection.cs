using System.Reflection;
using Application;
using Application.Authentication.Requests;
using Application.Authentication.Requests.Authorization;
using Microsoft.Extensions.Configuration;
using AutoMapper.Extensions.ExpressionMapping;
using Infrastructure.Spotify.Authentication;
using Infrastructure.Spotify.Authentication.Clients;
using Infrastructure.Spotify.Authentication.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Settings>(configuration.GetSection("Spotify:Authentication"));

            services.AddHttpClient<AuthenticationClient>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddExpressionMapping();
            });

            services.AddTransient<ISpotifyTokenService, SpotifyTokenService>();
            services.AddTransient<ISpotifyAuthenticationUriBuilder, AuthenticationUriBuilder>();
            services.AddSingleton<CodeProvider>();

            // TODO : temp implementation
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}