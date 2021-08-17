using Clipify.Application.Users;
using Clipify.Domain.Entities;

namespace Clipify.Application.Common
{
    public class BaseUserHandler
    {
        private readonly ICurrentUserService _currentUserService;

        private User? _currentUser;

        protected User CurrentUser => _currentUser ??= _currentUserService.GetCurrentUser();

        protected BaseUserHandler(ICurrentUserService currentUserService) =>
            _currentUserService = currentUserService;
    }
}