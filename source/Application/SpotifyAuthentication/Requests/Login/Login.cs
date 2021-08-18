using System.Threading;
using System.Threading.Tasks;
using Events.Authentication;
using MediatR;

namespace Application.SpotifyAuthentication.Requests.Login
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
            public static Response Failure() => new (true);
        };

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISpotifyTokenService _spotifyTokenService;
            private readonly IStateProvider _stateProvider;
            private readonly IEventBus _eventBus;

            public Handler(ISpotifyTokenService spotifyTokenService, IStateProvider stateProvider, IEventBus eventBus)
            {
                _spotifyTokenService = spotifyTokenService;
                _stateProvider = stateProvider;
                _eventBus = eventBus;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var (code, state) = request;
                if (!IsStateValid(state)) return Response.Failure();

                var tokens = await _spotifyTokenService.GetAccessTokenAsync(code);
                
                // TODO : store tokens
                await _eventBus.Publish(new LoggedIntoSpotify
                {
                    Tokens = tokens
                });

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