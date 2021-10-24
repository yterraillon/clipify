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
using Microsoft.Extensions.Logging;

namespace Application.Playlists.Commands.UpdateLocalPlaylists
{
    using static Constants;

    public class UpdateSpotifyLocalPlaylistService : UpdateLocalPlaylistsService
    {
        private readonly ISpotifyPlaylistService _spotifyPlaylistService;
        private readonly IRepository<Playlist> _playlistRepository;
        private readonly IDataReader<Playlist> _playlistDataReader;
        private readonly IEventBus _eventBus;
        private readonly ILogger<UpdateSpotifyLocalPlaylistService> _logger;

        public UpdateSpotifyLocalPlaylistService(ISpotifyPlaylistService spotifyPlaylistService, IRepository<Playlist> playlistRepository, IDataReader<Playlist> playlistDataReader, 
            IEventBus eventBus, ILogger<UpdateSpotifyLocalPlaylistService> logger)
        {
            _spotifyPlaylistService = spotifyPlaylistService;
            _playlistRepository = playlistRepository;
            _playlistDataReader = playlistDataReader;
            _eventBus = eventBus;
            _logger = logger;
        }

        public override async Task Handle(CancellationToken cancellationToken)
        {
            try
            {
                var allPlaylistInformations = (await _spotifyPlaylistService.GetAllPlaylists(cancellationToken)).ToList();
                var storedPlaylists = _playlistDataReader.GetAll().ToList();
                var storedPlaylistIds = storedPlaylists.Select(playlist => playlist.Id).ToList();

                AddNewPlaylistsToLocalDb(allPlaylistInformations, storedPlaylistIds);
                RemoveDeletedPlaylistsFromLocalDb(allPlaylistInformations, storedPlaylistIds);
                await CheckLocalPlaylistVersions(allPlaylistInformations, storedPlaylists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        protected override void AddNewPlaylistsToLocalDb(IEnumerable<PlaylistInformation> playlistInformations, ICollection<string> localPlaylistIds)
        {
            foreach (var playlistInformation in playlistInformations)
            {
                if (localPlaylistIds.Contains(playlistInformation.Id))
                    continue;

                var playlist = Playlist.Create(playlistInformation.Id, playlistInformation.Name, playlistInformation.Version,
                    playlistInformation.CoverImage, false, Services.Spotify);

                // NOTE : bulk Create maybe?
                _playlistRepository.Create(playlist);
            }
        }

        protected override void RemoveDeletedPlaylistsFromLocalDb(IEnumerable<PlaylistInformation> playlistInformations, IEnumerable<string> localPlaylistIds)
        {
            var spotifyPlaylistIds = playlistInformations.Select(p => p.Id).ToList();

            foreach (var localPlaylistId in localPlaylistIds)
            {
                if (spotifyPlaylistIds.Contains(localPlaylistId))
                    continue;

                _playlistRepository.Remove(localPlaylistId);
            }
        }

        protected override async Task CheckLocalPlaylistVersions(IList<PlaylistInformation> playlistInformations, IEnumerable<Playlist> localPlaylists)
        {
            foreach (var localPlaylist in localPlaylists)
            {
                var spotifyPlaylistInformation = playlistInformations.First(p => p.Id == localPlaylist.Id);

                if (localPlaylist.IsVersionUpToDateWithLatest(spotifyPlaylistInformation.Version))
                    continue;

                localPlaylist.UpdateWithLatestVersion(spotifyPlaylistInformation.Name, spotifyPlaylistInformation.Version, spotifyPlaylistInformation.CoverImage);

                // NOTE : bulk update maybe?
                _playlistRepository.Update(localPlaylist);

                await _eventBus.Publish(new PlaylistUpdated(localPlaylist.Id));
            }
        }
    }
}