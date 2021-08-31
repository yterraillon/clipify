using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Playlists;
using Application.SpotifyAuthentication;
using Domain;
using Infrastructure.Spotify.Webapi.Playlists.Models;

namespace Infrastructure.Spotify.Webapi.Playlists
{
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
            var tokens = await _tokensService.GetSpotifyTokens();
            var paginatedPlaylist =
                await _spotifyPlaylistClient.GetAListOfCurrentUsersPlaylist(tokens.AccessToken, cancellationToken);

            var simplifiedPlaylists = paginatedPlaylist.Items;
            return ToPlaylistInformations(simplifiedPlaylists);
        }

        private static IEnumerable<PlaylistInformation> ToPlaylistInformations(IEnumerable<SimplifiedPlaylistObject> simplifiedPlaylists) => 
            simplifiedPlaylists.Select(ToPlaylistInformation);

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