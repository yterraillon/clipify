using System;
using Application;
using Application.User;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.SpotifyAuthentication.Requests;
using Application.SpotifyAuthentication.Requests.GetAuthenticationUri;
using Application.SpotifyAuthentication.Requests.Login;
using Application.User.Commands.CreateLocalUserProfile;
using Domain.Entities;
using Infrastructure.Spotify.Webapi.UserProfile;

namespace Infrastructure
{
    using EventBus;
    using Spotify.Authentication;
    using Spotify.Authentication.AuthenticationUriBuilder;
    using Spotify.Authentication.Clients;
    using Database;
    using Database.Dtos;
    using SpotifyDomain = Domain.Entities.Spotify;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Spotify.Authentication.Settings>(configuration.GetSection("Spotify:Authentication"));
            services.Configure<Spotify.Webapi.Settings>(configuration.GetSection("Spotify:Webapi"));

            services.AddHttpClient<ISpotifyTokenService, TokenServiceClient>();
            services.AddHttpClient<ISpotifyUserProfileClient, UserProfileClient>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddExpressionMapping();
            });

            services.AddSingleton<IDbContext, DbContext>();
            services.AddTransient<IRepository<UserProfile, Guid>, Repository<UserProfile, UserDto, Guid>>();
            services.AddTransient<IRepository<SpotifyDomain.Tokens, Guid>, Repository<SpotifyDomain.Tokens, SpotifyTokensDto, Guid>>();

            services.AddTransient<ISpotifyAuthenticationUriBuilder, AuthenticationUriBuilder>();
            services.AddSingleton<IStateProvider, StateProvider>();
            services.AddSingleton<CodeProvider>();

            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.AddScoped<ICurrentUserService, CurrentUserService.CurrentUserService>();

            return services;
        }
    }
}