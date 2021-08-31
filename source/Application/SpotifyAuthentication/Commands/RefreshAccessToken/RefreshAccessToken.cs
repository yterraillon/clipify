using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Spotify;
using MediatR;

namespace Application.SpotifyAuthentication.Commands.RefreshAccessToken
{
    public static class RefreshAccessToken
    {
        public record Request : IRequest<Response>;

        public record Response
        {
            public bool IsSuccess { get; init; }

            public Response(bool isSuccess) => IsSuccess = isSuccess;

            public static Response Success() => new(true);

            // TODO : handle failures
            public static Response Failure() => new(false);
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository<Tokens> _tokensRepository;
            private readonly ISpotifyTokensClient _spotifyTokensClient;

            public Handler(IRepository<Tokens> tokensRepository, ISpotifyTokensClient spotifyTokensClient)
            {
                _tokensRepository = tokensRepository;
                _spotifyTokensClient = spotifyTokensClient;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var tokens = _tokensRepository.Get(Tokens.DefaultTokensId);
                var refreshToken = tokens.RefreshToken;

                (string accessToken, var expiresIn) = await _spotifyTokensClient.RefreshTokenAsync(refreshToken);
                tokens.RefreshAccessToken(accessToken, expiresIn);

                _tokensRepository.Update(tokens);

                return Response.Success();
            }
        }
    }
}