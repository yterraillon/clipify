using Application.Playlists;
using Application.SpotifyAuthentication;
using Infrastructure.Spotify.Webapi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Playlists.Queries.GetPlaylist;

namespace Infrastructure.Spotify.Webapi.Playlists
{
    using Models;

    public class PlaylistService : ISpotifyPlaylistService
    {
        private readonly ISpotifyPlaylistClient _spotifyPlaylistClient;
        private readonly ITokensService _tokensService;

        public PlaylistService(ISpotifyPlaylistClient spotifyPlaylistClient, ITokensService tokensService)
        {
            _spotifyPlaylistClient = spotifyPlaylistClient;
            _tokensService = tokensService;
        }

        public async Task<IEnumerable<PlaylistInformation>> GetAllPlaylists(CancellationToken cancellationToken)
        {
            var allPlaylistPages = await GetAllPaginatedPlaylists(cancellationToken);
            var playlistInformations = PlaylistServiceHelper.ExtractPlaylistInformations(allPlaylistPages);
            return playlistInformations;
        }

        public async Task<PlaylistViewModel> GetPlaylist(string playlistId, CancellationToken cancellationToken)
        {
            var tokens = await _tokensService.GetSpotifyTokens();
            var playlist = await _spotifyPlaylistClient.GetPlaylist(tokens.AccessToken, playlistId, cancellationToken);

            return new PlaylistViewModel
            {
                Id = playlist.Id,
                CoverImage = PlaylistServiceHelper.GetCoverImage(playlist.Images),
                Creator = playlist.Owner.DisplayName,
                Name = playlist.Name ?? string.Empty,
                Tracks = GetTrackViewModels(playlist)
            };

            static IEnumerable<TrackViewModel> GetTrackViewModels(PlaylistObject spotifyPlaylist) =>
                spotifyPlaylist.Tracks.Select(t =>
                    new TrackViewModel(
                        t.Track.Name, 
                        t.Track.Artists.Select(a => a.Name),
                        t.Track.Album.Name,
                        t.AddedAt));
        }

        private async Task<IEnumerable<PagingObject<SimplifiedPlaylistObject>>> GetAllPaginatedPlaylists(CancellationToken cancellationToken)
        {
            var tokens = await _tokensService.GetSpotifyTokens();

            var currentOffset = 0;
            var paginatedSimplifiedPlaylists = new List<PagingObject<SimplifiedPlaylistObject>>();

            var paginatedSimplifiedPlaylist = await GetPaginatedSimplifiedPlaylist(currentOffset);
            paginatedSimplifiedPlaylists.Add(paginatedSimplifiedPlaylist);

            while (paginatedSimplifiedPlaylist.Next != null && paginatedSimplifiedPlaylist.Items.Any())
            {
                currentOffset += paginatedSimplifiedPlaylist.Limit;
                paginatedSimplifiedPlaylist = await GetPaginatedSimplifiedPlaylist(currentOffset);
                paginatedSimplifiedPlaylists.Add(paginatedSimplifiedPlaylist);
            }

            return paginatedSimplifiedPlaylists;

            async Task<PagingObject<SimplifiedPlaylistObject>> GetPaginatedSimplifiedPlaylist(int offset) =>
                await _spotifyPlaylistClient.GetPaginatedListOfCurrentUsersPlaylist(tokens.AccessToken,
                    cancellationToken, 50, offset);
        }
    }
}