using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Clipify.Application.Playlists.Requests.GetPlaylist
{
    public static class GetPlaylist
    {
        public class Request : IRequest<PlaylistViewModel>
        {
            public string PlaylistId { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Request, PlaylistViewModel>
        {
            private readonly IPlaylistClient _client;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IPlaylistClient client, ICurrentUserService currentUser)
            {
                _client = client;
                _currentUserService = currentUser;
            }

            public async Task<PlaylistViewModel> Handle(Request request, CancellationToken cancellationToken)
            {
                if (!_currentUserService.IsUserLoggedIn())
                    return PlaylistViewModel.Empty;

                var user = _currentUserService.GetCurrentUser();

                return await _client.GetPlaylistAsync(user.AccessToken, user.UserId, request.PlaylistId,
                        cancellationToken);
            }
        }

        public class GetPlaylistValidator : AbstractValidator<GetPlaylist.Request>
        {
            public GetPlaylistValidator()
            {
                RuleFor(x => x.PlaylistId).NotEmpty().NotNull();
            }
        }
    }
}