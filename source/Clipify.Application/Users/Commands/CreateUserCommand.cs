using Clipify.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Users.Commands
{
    public static class CreateUser
    {
        public class Command : IRequest<Unit>
        {
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;

            public int ExpiresIn { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _userRepository.CreateUser(request.AccessToken, request.RefreshToken, request.ExpiresIn);

                return Task.FromResult(Unit.Value);
            }
        }
    }
}