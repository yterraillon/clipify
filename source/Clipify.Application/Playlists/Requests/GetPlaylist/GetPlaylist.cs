using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
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

        public class Handler : BaseHandler, IRequestHandler<Request, PlaylistViewModel>
        {
            private readonly IPlaylistClient _client;

            public Handler(IPlaylistClient client, ICurrentUserService currentUserService) : base(currentUserService)
            {
                _client = client;
            }

            public async Task<PlaylistViewModel> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _client.GetPlaylistAsync(CurrentUser.AccessToken, CurrentUser.UserId, request.PlaylistId,
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