using AutoMapper.Extensions.ExpressionMapping;
using Clipify.Application;
using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.Authorization;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Commands.SyncPlaylists;
using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database;
using Clipify.Infrastructure.Database.Dtos;
using Clipify.Infrastructure.Database.Repositories;
using Clipify.Infrastructure.Jobs;
using Clipify.Infrastructure.Spotify.Playlists;
using Clipify.Infrastructure.Spotify.Settings;
using Clipify.Infrastructure.Spotify.UserProfile;
using Clipify.Infrastructure.SpotifyAuth;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Reflection;

namespace Clipify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddClipifyInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SpotifyAuthSettings>(configuration.GetSection("SpotifyAuth"));
            services.Configure<SpotifyApiSettings>(configuration.GetSection("SpotifyEndpoints"));

            services.AddHttpClient<SpotifyAuthClient>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddExpressionMapping();
            });

            services.AddSingleton<IDbContext, DbContext>();

            services.AddTransient<IPlaylistClient, PlaylistClient>();
            services.AddTransient<IPlaylistService, PlaylistService>();

            // Repositories
            services.AddTransient<IRepository<User, string>, Repository<User, UserDto, string>>();
            services.AddTransient<IRepository<Playlist, string>, Repository<Playlist, PlaylistDto, string>>();
            services.AddTransient<IRepository<Track, string>, Repository<Track, TrackDto, string>>();
            services.AddTransient<IRepository<ForkedPlaylist, string>, Repository<ForkedPlaylist, ForkedPlaylistDto, string>>();

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IUserProfileClient, UserProfileClient>();

            services.AddTransient<IAuthService, SpotifyAuthService>();
            services.AddTransient<IAuthUriBuilder, SpotifyAuthUriBuilder>();
            services.AddSingleton<IAuthCodeProvider, SpotifyAuthCodeProvider>();

            // Quartz
            services.AddQuartz(quartz =>
            {
                const string jobKey = "ClipifySyncJob";
                quartz
                    .AddJob<SyncJob>(new JobKey(jobKey))
                    .AddTrigger(options =>
                    {
                        options.ForJob(jobKey);
                        options.StartNow()
                            .WithSchedule(SimpleScheduleBuilder
                                .Create()
                                .WithIntervalInHours(24)
                                .RepeatForever());
                    });
            });
            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}