using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Requests.GetPlaylist
{
    public static class GetPlaylist
    {
        public record Request(string PlaylistId) : IRequest<PlaylistViewModel>
        {
            public override string ToString()
            {
                return $"{nameof(PlaylistId)}: {PlaylistId}";
            }
        }

        public class Handler : BaseUserHandler, IRequestHandler<Request, PlaylistViewModel>
        {
            private readonly IPlaylistClient _client;

            public Handler(IPlaylistClient client, ICurrentUserService currentUserService) : base(currentUserService)
              => _client = client;

            public async Task<PlaylistViewModel> Handle(Request request, CancellationToken cancellationToken)
              => await _client.GetPlaylistAsync(CurrentUser.AccessToken, CurrentUser.UserId, request.PlaylistId, cancellationToken);
        }

        public class GetPlaylistValidator : AbstractValidator<GetPlaylist.Request>
        {
            public GetPlaylistValidator()
            {
                RuleFor(x => x.PlaylistId)
                    .NotEmpty()
                    .NotNull();
            }
        }
    }
}