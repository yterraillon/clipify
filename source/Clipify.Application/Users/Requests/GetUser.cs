using System.Threading;
using System.Threading.Tasks;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Users.Requests
{
    public static class GetUser
    {
        public class Request : IRequest<User>
        {

        }

        public class Handler : IRequestHandler<Request, User>
        {
            private readonly ICurrentUserService _currentUserService;

            public Handler(ICurrentUserService currentUserService)
            {
                _currentUserService = currentUserService;
            }

            public Task<User> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(_currentUserService.GetCurrentUser());
        }
    }
}