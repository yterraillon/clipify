using Clipify.Application;
using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.Authorization;
using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database;
using Clipify.Infrastructure.Database.Dtos;
using Clipify.Infrastructure.Database.Repositories;
using Clipify.Infrastructure.Spotify.UserProfile;
using Clipify.Infrastructure.SpotifyAuth;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper.Extensions.ExpressionMapping;

namespace Clipify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SpotifyAuthSettings>(configuration.GetSection("SpotifyAuth"));

            services.AddHttpClient<SpotifyAuthClient>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddExpressionMapping();
            });

            services.AddSingleton<IDbContext, DbContext>();
            services.AddTransient<IUserProfileClient, UserProfileClient>();
            services.AddSingleton<IAuthCodeProvider, SpotifyAuthCodeProvider>();

            // Repositories
            services.AddTransient<IRepository<User, string>, Repository<User, UserDto, string>>();

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IAuthService, SpotifyAuthService>();
            services.AddTransient<IAuthUriBuilder, SpotifyAuthUriBuilder>();

            return services;
        }
    }
}