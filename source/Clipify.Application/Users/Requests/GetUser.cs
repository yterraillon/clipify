using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Users.Requests
{
    public static class GetUser
    {
        public class Request : IRequest<User> { }

        public class Handler : BaseHandler, IRequestHandler<Request, User>
        {
            public Handler(ICurrentUserService currentUserService) : base(currentUserService) { }

            public Task<User> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(CurrentUser);
        }
    }
}