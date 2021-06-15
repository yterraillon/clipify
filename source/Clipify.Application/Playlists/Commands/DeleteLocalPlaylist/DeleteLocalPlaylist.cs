using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Playlists.Commands.DeleteLocalPlaylist
{
    public static class DeleteLocalPlaylist
    {
        public class Command : IRequest<bool>
        {
            public string PlaylistId { get; set; } = string.Empty;
        }

        public class Handler : BaseUserHandler, IRequestHandler<Command, bool>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            public Handler(IRepository<Playlist, string> playlistRepository, ICurrentUserService currentUserService) : base(currentUserService)
            {
                _playlistRepository = playlistRepository;
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
                => Task.FromResult(_playlistRepository.Remove(request.PlaylistId));
        }
    }
}