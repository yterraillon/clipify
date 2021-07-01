using Clipify.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Requests.GetLocalPlaylists
{
    public static class GetLocalPlaylists
    {
        public record Request : IRequest<IEnumerable<Playlist>>;

        public class Handler : IRequestHandler<Request, IEnumerable<Playlist>>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            public Handler(IRepository<Playlist, string> playlistRepository)
                => _playlistRepository = playlistRepository;

            public Task<IEnumerable<Playlist>> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(_playlistRepository.GetAll());
        }
    }
}