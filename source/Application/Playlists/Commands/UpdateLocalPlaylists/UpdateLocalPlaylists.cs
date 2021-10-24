using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Playlists.Commands.UpdateLocalPlaylists
{
    public static class UpdateLocalPlaylists
    {
        public record Request : IRequest<Response>;
        public record Response
        {
            public bool IsSuccess { get; init; }

            public Response(bool isSuccess) => IsSuccess = isSuccess;

            public static Response Success() => new(true);
            public static Response Failure() => new(false);
        };

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly UpdateSpotifyLocalPlaylistService _updateSpotifyLocalPlaylistService;
            private readonly ILogger<Handler> _logger;

            public Handler(UpdateSpotifyLocalPlaylistService updateSpotifyLocalPlaylistService, ILogger<Handler> logger)
            {
                _updateSpotifyLocalPlaylistService = updateSpotifyLocalPlaylistService;
                _logger = logger;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    await _updateSpotifyLocalPlaylistService.Handle(cancellationToken);

                    return Response.Success();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return Response.Failure();
                }
            }
        }
    }
}