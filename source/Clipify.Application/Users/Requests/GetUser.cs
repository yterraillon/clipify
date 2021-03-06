using Clipify.Application.Common;
using Clipify.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Users.Requests
{
    public static class GetUser
    {
        public record Request : IRequest<User>;

        public class Handler : BaseUserHandler, IRequestHandler<Request, User>
        {
            public Handler(ICurrentUserService currentUserService) : base(currentUserService) { }

            public Task<User> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(CurrentUser);
        }
    }
}