using System.Threading;
using System.Threading.Tasks;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Playlists.Commands.ForkPlaylist
{
    public static class ForkPlaylist
    {
        public class Request : IRequest
        {
            public string OriginalPlaylistId { get; set; } = string.Empty;
        }
        
        public class Handler : IRequestHandler<Request>
        {
            public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                // Infra forkedPlaylistId = SpotifyPlaylist.Create(new playlist (name, photo))
                // obtention d'un forked playlist id 
                ForkedPlaylist.Create(forkedPlaylistId, request.OriginalPlaylistId);
                //stockage en base de la forked playlist
                
                // alimenter la playlist
                // get OriginalPlaylistTracks
                // Add all tracks to forked playlist
                
                throw new System.NotImplementedException();
            }

            // forkedPlaylist.getFromDb
            // forkedPlaylist.IsOutdated? 
            // si oui, synchro, si non : skip 

            // Get les nouvelles racks depuis l'originale
            // add tracks to forked

            // RegisterLastSync
            //forked playlist.Update en base
        }
    }
}