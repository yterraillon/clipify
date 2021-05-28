using Clipify.Application.Auth.Requests;
using Clipify.Application.Users;
using Clipify.Infrastructure.Database;
using Clipify.Infrastructure.Database.Repositories;
using Clipify.Infrastructure.SpotifyAuth;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Clipify.Application;
using Clipify.Application.Auth.Requests.Authorization;
using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;
using Clipify.Infrastructure.Spotify.UserProfile;
using Clipify.Infrastructure.SpotifyAuth.Settings;

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
            services.AddSingleton<IUserProfileClient, UserProfileClient>();
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