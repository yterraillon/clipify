using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Playlists.Commands.UpdateLocalPlaylists
{
    public abstract class UpdateLocalPlaylistsService
    {
        public abstract Task Handle(CancellationToken cancellationToken);

        protected abstract void AddNewPlaylistsToLocalDb(IEnumerable<PlaylistInformation> playlistInformations,
            ICollection<string> localPlaylistIds);

        protected abstract void RemoveDeletedPlaylistsFromLocalDb(IEnumerable<PlaylistInformation> playlistInformations,
            IEnumerable<string> localPlaylistIds);

        protected abstract Task CheckLocalPlaylistVersions(IList<PlaylistInformation> playlistInformations,
            IEnumerable<Playlist> localPlaylists);
    }
}