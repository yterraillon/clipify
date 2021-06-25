using Clipify.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.ForkPlaylist
{
    public static class ForkPlaylist
    {
        public record Request(string OriginalPlaylistId) : IRequest;

        public class Handler : AsyncRequestHandler<Request>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;
            private readonly IRepository<ForkedPlaylist, string> _forkedRepository;

            public Handler(IRepository<Playlist, string> playlistRepository, IRepository<ForkedPlaylist, string> forkedRepository)
            {
                _playlistRepository = playlistRepository;
                _forkedRepository = forkedRepository;
            }

            protected override Task Handle(Request request, CancellationToken cancellationToken)
            {
                _forkedRepository.Add(ForkedPlaylist.Create(request.OriginalPlaylistId));

                return Task.CompletedTask;
            }
        }
    }
}