using Application.User;
using Domain.Entities;

namespace Infrastructure.Database
{
    public class CurrentUserService : ICurrentUserService
    {
        public User GetCurrentUser()
        {
            return new User();
        }

        public bool IsUserLoggedIn()
        {
            return false;
        }
    }
}