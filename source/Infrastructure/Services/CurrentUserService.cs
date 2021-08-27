using Application;
using Application.User;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private UserProfile CurrentUser { get; }

        public CurrentUserService(IRepository<UserProfile> userRepository)
        {
            CurrentUser = userRepository.Get(UserProfile.DefaultUserId);
        }

        public UserProfile GetCurrentUser() => CurrentUser;

        public bool IsUserLoggedIn() => CurrentUser.Username != string.Empty;
    }
}