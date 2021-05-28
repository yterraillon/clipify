using System.Collections.Generic;
using Clipify.Domain;

namespace Clipify.Application.Playlists.Commands
{
    public interface IPlaylistService
    {
        IEnumerable<Track> GetAllTracks();
    }
}