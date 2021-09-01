using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Playlists;
using Application.SpotifyAuthentication;
using Domain;
using Infrastructure.Spotify.Webapi.Models;

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
            var playlistInformations = ExtractPlaylistInformations(allPlaylistPages);
            return playlistInformations;
        }

        private async Task<IEnumerable<PagingObject<SimplifiedPlaylistObject>>> GetAllPaginatedPlaylists(CancellationToken cancellationToken)
        {
            var tokens = await _tokensService.GetSpotifyTokens();

            var offset = 0;
            var paginatedSimplifiedPlaylists = new List<PagingObject<SimplifiedPlaylistObject>>();

            var paginatedSimplifiedPlaylist = await GetPaginatedSimplifiedPlaylist(offset);
            paginatedSimplifiedPlaylists.Add(paginatedSimplifiedPlaylist);

            while (paginatedSimplifiedPlaylist.Next != null && paginatedSimplifiedPlaylist.Items.Any())
            {
                offset += paginatedSimplifiedPlaylist.Limit;
                paginatedSimplifiedPlaylist = await GetPaginatedSimplifiedPlaylist(offset);
                paginatedSimplifiedPlaylists.Add(paginatedSimplifiedPlaylist);
            }

            return paginatedSimplifiedPlaylists;

            async Task<PagingObject<SimplifiedPlaylistObject>> GetPaginatedSimplifiedPlaylist(int offset) =>
                await _spotifyPlaylistClient.GetAPaginatedListOfCurrentUsersPlaylist(tokens.AccessToken,
                    cancellationToken, 50, offset);
        }

        private static IEnumerable<PlaylistInformation> ExtractPlaylistInformations(IEnumerable<PagingObject<SimplifiedPlaylistObject>> allPlaylistsPages)
        {
            var playlistInformations = new List<PlaylistInformation>();
            foreach (var playlistsPage in allPlaylistsPages)
            {
                var items = playlistsPage.Items;
                playlistInformations.AddRange(ToPlaylistInformations(items));

                var shazam = items.FirstOrDefault(i => i.Name.Contains("Shazam"));
            }

            return playlistInformations;
        }

        private static IEnumerable<PlaylistInformation> ToPlaylistInformations(IEnumerable<SimplifiedPlaylistObject> simplifiedPlaylists) =>
            simplifiedPlaylists.Select(ToPlaylistInformation).ToList();

        private static PlaylistInformation ToPlaylistInformation(SimplifiedPlaylistObject simplifiedPlaylist) =>
            new()
            {
                Id = simplifiedPlaylist.Id,
                Name = simplifiedPlaylist.Name,
                Version = simplifiedPlaylist.SnapshotId,
                CoverImage = simplifiedPlaylist.Images.FirstOrDefault()?.Url ?? "about:blank"
            };
    }
}