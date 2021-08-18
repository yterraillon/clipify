using Application;
using Application.User;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.SpotifyAuthentication.Requests;
using Application.SpotifyAuthentication.Requests.GetAuthenticationUri;
using Application.SpotifyAuthentication.Requests.Login;

namespace Infrastructure
{
    using EventBus;
    using Spotify.Authentication;
    using Spotify.Authentication.AuthenticationUriBuilder;
    using Spotify.Authentication.Clients;
    using Database;
    using Spotify.Webapi;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Spotify.Authentication.Settings>(configuration.GetSection("Spotify:Authentication"));
            services.Configure<Spotify.Webapi.Settings>(configuration.GetSection("Spotify:Webapi"));

            services.AddHttpClient<ISpotifyTokenService, TokenServiceClient>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddExpressionMapping();
            });

            services.AddTransient<ISpotifyAuthenticationUriBuilder, AuthenticationUriBuilder>();
            services.AddSingleton<IStateProvider, StateProvider>();
            services.AddSingleton<CodeProvider>();

            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}