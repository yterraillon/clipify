using Clipify.Application.Common.Interfaces;
using Clipify.Infrastructure.Spotify;
using Microsoft.Extensions.DependencyInjection;

namespace Clipify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISpotifyAuthService, SpotifyAuthService>();

            return services;
        }
    }
}