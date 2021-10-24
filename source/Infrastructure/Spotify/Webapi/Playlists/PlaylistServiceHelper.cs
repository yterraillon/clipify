using System.Collections.Generic;
using System.Linq;
using Application.Playlists;
using Infrastructure.Spotify.Webapi.Models;

namespace Infrastructure.Spotify.Webapi.Playlists
{
    using Models;

    public static class PlaylistServiceHelper
    {
        public static IEnumerable<PlaylistInformation> ExtractPlaylistInformations(IEnumerable<PagingObject<SimplifiedPlaylistObject>> allPlaylistsPages)
        {
            var playlistInformations = new List<PlaylistInformation>();
            foreach (var playlistsPage in allPlaylistsPages)
            {
                var items = playlistsPage.Items;
                playlistInformations.AddRange(ToPlaylistInformations(items));
            }

            return playlistInformations;
        }

        private static IEnumerable<PlaylistInformation> ToPlaylistInformations(IEnumerable<SimplifiedPlaylistObject> simplifiedPlaylists) =>
            simplifiedPlaylists.Select(ToPlaylistInformation).ToList();

        private static PlaylistInformation ToPlaylistInformation(SimplifiedPlaylistObject simplifiedPlaylist) =>
            new()
            {
                Id = simplifiedPlaylist.Id,
                Name = simplifiedPlaylist.Name ?? string.Empty,
                Version = simplifiedPlaylist.SnapshotId,
                CoverImage = GetCoverImage(simplifiedPlaylist.Images)
            };

        public static string GetCoverImage(IEnumerable<ImageObject> images) =>
            images.FirstOrDefault()?.Url ?? "about:blank";
    }
}