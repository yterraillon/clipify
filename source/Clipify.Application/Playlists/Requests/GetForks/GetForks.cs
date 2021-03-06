using Clipify.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Requests.GetForks
{
    public static class GetForks
    {
        public record Request : IRequest<IEnumerable<ForkedPlaylist>>;

        public class Handler : IRequestHandler<Request, IEnumerable<ForkedPlaylist>>
        {
            private readonly IRepository<ForkedPlaylist, string> _forkedRepository;

            public Handler(IRepository<ForkedPlaylist, string> forkedRepository)
                => _forkedRepository = forkedRepository;

            public Task<IEnumerable<ForkedPlaylist>> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(_forkedRepository.GetAll());
        }
    }
}