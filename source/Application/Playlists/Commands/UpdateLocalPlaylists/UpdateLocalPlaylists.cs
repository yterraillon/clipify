using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain;
using Domain.Entities;
using Events.Playlists;
using MediatR;

namespace Application.Playlists.Commands.UpdateLocalPlaylists
{
    using static Constants;

    // NOTE : should probably have a SpotifyService, an Apple Music Service etc.. So this one just orchestrates the other ones
    public static class UpdateLocalPlaylists
    {
        public record Request : IRequest<Response>;
        public record Response
        {
            public bool IsSuccess { get; init; }

            public Response(bool isSuccess) => IsSuccess = isSuccess;

            public static Response Success() => new(true);
            public static Response Failure() => new(false);
        };

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISpotifyPlaylistService _spotifyPlaylistService;
            private readonly IRepository<Playlist> _playlistRepository;
            private readonly IDataReader<Playlist> _playlistDataReader;
            private readonly IEventBus _eventBus;

            public Handler(ISpotifyPlaylistService spotifyPlaylistService, IRepository<Playlist> playlistRepository, IDataReader<Playlist> playlistDataReader, IEventBus eventBus)
            {
                _spotifyPlaylistService = spotifyPlaylistService;
                _playlistRepository = playlistRepository;
                _playlistDataReader = playlistDataReader;
                _eventBus = eventBus;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var allPlaylistInformations = (await _spotifyPlaylistService.GetAllPlaylists(cancellationToken)).ToList();
                    var storedPlaylists = _playlistDataReader.GetAll().ToList();
                    var storedPlaylistIds = storedPlaylists.Select(playlist => playlist.Id).ToList();

                    AddNewPlaylistsToLocalDb(allPlaylistInformations, storedPlaylistIds);
                    RemoveDeletedPlaylistsFromLocalDb(allPlaylistInformations, storedPlaylistIds);
                    await CheckLocalPlaylistVersions(allPlaylistInformations, storedPlaylists);

                    return Response.Success();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return Response.Failure();
                }
            }

            private void AddNewPlaylistsToLocalDb(IEnumerable<PlaylistInformation> spotifyPlaylistInformations, ICollection<string> localPlaylistIds)
            {
                foreach (var playlistInformation in spotifyPlaylistInformations)
                {
                    if (localPlaylistIds.Contains(playlistInformation.Id))
                        continue;

                    var playlist = Playlist.Create(playlistInformation.Id, playlistInformation.Name, playlistInformation.Version,
                        playlistInformation.CoverImage, false, Services.Spotify);

                    // NOTE : bulk Create maybe?
                    _playlistRepository.Create(playlist);
                }
            }

            private void RemoveDeletedPlaylistsFromLocalDb(IEnumerable<PlaylistInformation> spotifyPlaylistInformations,
                IEnumerable<string> localPlaylistIds)
            {
                var spotifyPlaylistIds = spotifyPlaylistInformations.Select(p => p.Id).ToList();

                foreach (var localPlaylistId in localPlaylistIds)
                {
                    if(spotifyPlaylistIds.Contains(localPlaylistId))
                        continue;

                    _playlistRepository.Remove(localPlaylistId);
                }
            }

            private async Task CheckLocalPlaylistVersions(IList<PlaylistInformation> spotifyPlaylistInformations,
                IEnumerable<Playlist> localPlaylists)
            {
                foreach (var localPlaylist in localPlaylists)
                {
                    var spotifyPlaylistInformation = spotifyPlaylistInformations.First(p => p.Id == localPlaylist.Id);

                    if(localPlaylist.IsVersionUpToDateWithLatest(spotifyPlaylistInformation.Version))
                        continue;

                    localPlaylist.UpdateWithLatestVersion(spotifyPlaylistInformation.Name, spotifyPlaylistInformation.Version, spotifyPlaylistInformation.CoverImage);

                    // NOTE : bulk update maybe?
                    _playlistRepository.Update(localPlaylist);

                    await _eventBus.Publish(new PlaylistUpdated(localPlaylist.Id));
                }
            }
        }
    }
}