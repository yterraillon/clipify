using Clipify.Application.Common;
using Clipify.Application.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Profile.Requests.GetProfile
{
    using Models;

    public static class GetProfile
    {
        public class Request : IRequest<ProfileResponse> { }

        public class Handler : BaseUserHandler, IRequestHandler<Request, ProfileResponse>
        {
            private readonly IUserProfileClient _client;

            public Handler(IUserProfileClient client, ICurrentUserService currentUserService) : base(currentUserService)
                => _client = client;

            public Task<ProfileResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = CurrentUser.AccessToken;

                return string.IsNullOrEmpty(token)
                    ? Task.FromResult(ProfileResponse.Empty)
                    : _client.GetUserProfileAsync(token, cancellationToken);
            }
        }
    }
}