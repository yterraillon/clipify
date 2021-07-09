using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.ForkPlaylist
{
    public static class ForkPlaylist
    {
        public record Request(string Name, string OriginalPlaylistId) : IRequest<PlaylistViewModel>
        {
            /// <inheritdoc />
            public override string ToString()
            {
                return $"{nameof(OriginalPlaylistId)}: {OriginalPlaylistId}";
            }
        }

        public class Handler : BaseUserHandler, IRequestHandler<Request, PlaylistViewModel>
        {
            private readonly IRepository<ForkedPlaylist, string> _forkedRepository;

            private readonly IRepository<Track, string> _trackRepository;
            
            private readonly IPlaylistClient _playlistClient;

            public Handler(ICurrentUserService currentUserService, IRepository<ForkedPlaylist, string> forkedRepository,
                IPlaylistClient playlistClient, IRepository<Track, string> trackRepository) : base(currentUserService)
            {
                _forkedRepository = forkedRepository;
                _playlistClient = playlistClient;
                _trackRepository = trackRepository;
            }

            /// <inheritdoc />
            public async Task<PlaylistViewModel> Handle(Request request, CancellationToken cancellationToken)
            {
                _forkedRepository.Add(ForkedPlaylist.Create(request.Name, request.OriginalPlaylistId));
                
                var response = await _playlistClient.CreatePlaylistAsync(CurrentUser.AccessToken, CurrentUser.UserId, request.Name,
                    cancellationToken);
                var tracks = _trackRepository.GetAll(x => x.PlaylistId == request.OriginalPlaylistId);

                await _playlistClient.AddTracksToPlaylistAsync(CurrentUser.AccessToken, response.Id, tracks,
                    cancellationToken);
                
                return response;

            }
        }
    }
}