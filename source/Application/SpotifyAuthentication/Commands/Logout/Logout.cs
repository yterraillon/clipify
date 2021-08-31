using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Spotify;
using Events.Authentication;
using MediatR;

namespace Application.SpotifyAuthentication.Commands.Logout
{
    public static class Logout
    {
        public record Request : IRequest<Response>;

        public record Response
        {
            public bool IsSuccess { get; init; }

            public Response(bool isSuccess) => IsSuccess = isSuccess;

            public static Response Success() => new(true);
            public static Response Failure() => new(false);
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventBus _eventBus;
            private readonly IRepository<Tokens> _tokensRepository;

            public Handler(IRepository<Tokens> tokensRepository, IEventBus eventBus)
            {
                _tokensRepository = tokensRepository;
                _eventBus = eventBus;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                _tokensRepository.Remove(Tokens.DefaultTokensId);
                _eventBus.Publish(new SpotifySignedOut());

                return Task.FromResult(Response.Success());
            }
        }
    }
}