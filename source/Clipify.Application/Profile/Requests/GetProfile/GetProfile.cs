using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Users;
using MediatR;

namespace Clipify.Application.Profile.Requests.GetProfile
{
    using Models;

    public static class GetProfile
    {
        public class Request : IRequest<ProfileResponse>
        {
        }

        public class Handler : IRequestHandler<Request, ProfileResponse>
        {
            private readonly IUserProfileClient _client;
            private readonly ICurrentUserService _currentUser;

            public Handler(IUserProfileClient client, ICurrentUserService currentUser)
            {
                _client = client;
                _currentUser = currentUser;
            }

            public Task<ProfileResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = _currentUser.GetCurrentUser()?.AccessToken;

                return string.IsNullOrEmpty(token)
                    ? Task.FromResult(ProfileResponse.Empty)
                    : _client.GetUserProfileAsync(token, cancellationToken);
            }
        }
    }
}