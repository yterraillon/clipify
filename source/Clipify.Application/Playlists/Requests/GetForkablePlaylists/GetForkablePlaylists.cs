using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Playlists.Requests.GetForkablePlaylists.Models;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Playlists.Requests.GetForkablePlaylists
{
    public static class GetForkablePlaylists
    {
        public record Request : IRequest<Response>;

        public record Response(IReadOnlyCollection<ForkablePlaylistViewModel> ForkablePlaylists);

        public class Handler : BaseUserHandler, IRequestHandler<Request, Response>
        {
            private readonly IPlaylistClient _playlistClient;
            private readonly IRepository<ForkedPlaylist, string> _forkedPlaylistsRepository;

            public Handler(IPlaylistClient playlistClient, IRepository<ForkedPlaylist, string> forkedPlaylistsRepository, ICurrentUserService currentUserService) : base(currentUserService)
            {
                _playlistClient = playlistClient;
                _forkedPlaylistsRepository = forkedPlaylistsRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var userPlaylists = await _playlistClient.GetPlaylistsAsync(CurrentUser.AccessToken, CurrentUser.UserId,
                    cancellationToken);

                var forkedPlaylists = _forkedPlaylistsRepository.GetAll();
                var forkablePlaylists = GetForkablePlaylists(userPlaylists, forkedPlaylists);

                var forkablePlaylistsViewModels = MapToForkablePlaylistViewModel(forkablePlaylists);
                return new Response(forkablePlaylistsViewModels);
            }

            private static IEnumerable<PlaylistViewModel> GetForkablePlaylists(
                IEnumerable<PlaylistViewModel> userPlaylists, IEnumerable<ForkedPlaylist> alreadyForkedPlaylists) =>
                userPlaylists.Where(p => alreadyForkedPlaylists.All(f => f.OriginalPlaylistId != p.Id));

            private static IReadOnlyCollection<ForkablePlaylistViewModel> MapToForkablePlaylistViewModel(IEnumerable<PlaylistViewModel> playlists) =>
                playlists.Select(p => new ForkablePlaylistViewModel
                {
                    Description = p.Description,
                    Id = p.Id,
                    Images = p.Images,
                    Name = p.Name,
                    Url = p.Url
                }).ToList().AsReadOnly();
        }
    }
}