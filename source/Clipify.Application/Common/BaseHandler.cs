using Clipify.Application.Users;
using Clipify.Domain.Entities;

namespace Clipify.Application.Common
{
    public class BaseHandler
    {
        private readonly ICurrentUserService _currentUserService;

        private User? _currentUser = null;

        protected User CurrentUser => _currentUser ??= _currentUserService.GetCurrentUser();

        public BaseHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
    }
}