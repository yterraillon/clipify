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
                throw new System.NotImplementedException();
            }
        }
    }
}