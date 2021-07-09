using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Requests.GetPlaylistWithTracks
{
    public static class GetPlaylistWithTracks
    {
        public record Request(string PlaylistId) : IRequest<PlaylistViewModel>;
        
        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {  
                RuleFor(x => x.PlaylistId)
                    .NotNull()
                    .NotEmpty();
            }
        }
        
        public class Handler : BaseUserHandler, IRequestHandler<Request, PlaylistViewModel>
        {
            private readonly IPlaylistClient _playlistClient;

            public Handler(IPlaylistClient playlistClient, ICurrentUserService currentUserService) : base(currentUserService)
                => _playlistClient = playlistClient;

            /// <inheritdoc />
            public Task<PlaylistViewModel> Handle(Request request, CancellationToken cancellationToken)
                => _playlistClient.GetPlaylistWithTracksAsync(CurrentUser.AccessToken, request.PlaylistId, cancellationToken);
        }
    }
}