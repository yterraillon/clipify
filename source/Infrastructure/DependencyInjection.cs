using Application;
using Application.User;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.SpotifyAuthentication;
using Application.SpotifyAuthentication.Commands;
using Application.SpotifyAuthentication.Commands.Login;
using Application.SpotifyAuthentication.Queries.GetAuthenticationUri;
using Application.User.Commands.CreateLocalUserProfile;
using Domain.Entities;

namespace Infrastructure
{
    using EventBus;
    using Services;
    using Spotify.Authentication;
    using Spotify.Authentication.AuthenticationUriBuilder;
    using Spotify.Authentication.Clients;
    using Spotify.Webapi.UserProfile;
    using Database;
    using Database.Dtos;
    using SpotifyEntities = Domain.Entities.Spotify;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Spotify.Authentication.Settings>(configuration.GetSection("Spotify:Authentication"));
            services.Configure<Spotify.Webapi.Settings>(configuration.GetSection("Spotify:Webapi"));

            services.AddHttpClient<ISpotifyTokensClient, TokensClient>();
            services.AddHttpClient<ISpotifyUserProfileClient, UserProfileClient>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddExpressionMapping();
            });

            services.AddSingleton<IDbContext, DbContext>();
            services.AddTransient<IRepository<UserProfile>, Repository<UserProfile, UserDto>>();
            services.AddTransient<IRepository<SpotifyEntities.Tokens>, Repository<SpotifyEntities.Tokens, SpotifyTokensDto>>();

            services.AddTransient<ISpotifyAuthenticationUriBuilder, AuthenticationUriBuilder>();
            services.AddSingleton<IStateProvider, StateProvider>();
            services.AddSingleton<CodeProvider>();

            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<ITokensService, SpotifyTokensService>();

            return services;
        }
    }
}