using Clipify.Application.Common;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.DeleteLocalPlaylist
{
    public static class DeleteLocalPlaylist
    {
        public record Command(string PlaylistId) : IRequest<bool>
        {
            /// <inheritdoc />
            public override string ToString()
            {
                return $"{nameof(PlaylistId)}: {PlaylistId}";
            }
        }

        public class Handler : BaseUserHandler, IRequestHandler<Command, bool>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            public Handler(IRepository<Playlist, string> playlistRepository, ICurrentUserService currentUserService) : base(currentUserService)
                => _playlistRepository = playlistRepository;

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
                => Task.FromResult(_playlistRepository.Remove(request.PlaylistId));
        }
    }
}