using Application.User;

namespace Application.Common
{
    using Domain.Entities;

    public class BaseUserHandler
    {
        private readonly ICurrentUserService _currentUserService;

        private UserProfile? _currentUser;

        protected UserProfile CurrentUser => _currentUser ??= _currentUserService.GetCurrentUser();

        protected BaseUserHandler(ICurrentUserService currentUserService) =>
            _currentUserService = currentUserService;
    }
}