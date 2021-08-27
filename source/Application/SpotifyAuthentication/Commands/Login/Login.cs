using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Spotify;
using Events.Authentication;
using MediatR;

namespace Application.SpotifyAuthentication.Commands.Login
{
    public static class Login
    {
        public record Request(string Code, string State) : IRequest<Response>
        {
            public override string ToString() => $"{nameof(Code)}: {Code}";
        }

        public record Response
        {
            public bool IsSuccess { get; init; }

            public Response(bool isSuccess) => IsSuccess = isSuccess;

            public static Response Success() => new (true);
            public static Response Failure() => new (false);
        };

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISpotifyTokensClient _spotifyTokensClient;
            private readonly IStateProvider _stateProvider;
            private readonly IEventBus _eventBus;
            private readonly IRepository<Tokens> _tokensRepository;

            public Handler(ISpotifyTokensClient spotifyTokensClient, IStateProvider stateProvider, IEventBus eventBus, IRepository<Tokens> tokensRepository)
            {
                _spotifyTokensClient = spotifyTokensClient;
                _stateProvider = stateProvider;
                _eventBus = eventBus;
                _tokensRepository = tokensRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var (code, state) = request;
                if (!IsStateValid(state)) return Response.Failure();

                (string accessToken, string refreshToken, var expiresIn) = await _spotifyTokensClient.GetAccessTokenAsync(code);
                var tokens = Tokens.Create(accessToken, refreshToken, expiresIn);
                _tokensRepository.Create(tokens);

                await _eventBus.Publish(new LoggedInWithSpotify(tokens.AccessToken, tokens.ExpirationDate));
                return Response.Success();
            }

            private bool IsStateValid(string state)
            {
                var expectedState = _stateProvider.State;
                return expectedState == state;
            }
        }
    }
}