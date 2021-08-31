using System.Threading.Tasks;
using Application;
using Application.SpotifyAuthentication;
using Application.SpotifyAuthentication.Commands.RefreshAccessToken;
using Domain.Entities.Spotify;
using MediatR;

namespace Infrastructure.Services
{
    public class SpotifyTokensService : ITokensService
    {
        private readonly IRepository<Tokens> _tokensRepository;
        private readonly IMediator _mediator;

        public SpotifyTokensService(IRepository<Tokens> tokensRepository, IMediator mediator)
        {
            _tokensRepository = tokensRepository;
            _mediator = mediator;
        }

        public async Task<Tokens> GetSpotifyTokens()
        {
            var tokens = _tokensRepository.Get(Tokens.DefaultTokensId);

            if (!tokens.AreExpired())
                return tokens;

            await _mediator.Send(new RefreshAccessToken.Request());
            tokens = _tokensRepository.Get(Tokens.DefaultTokensId);

            return tokens;
        }
    }
}