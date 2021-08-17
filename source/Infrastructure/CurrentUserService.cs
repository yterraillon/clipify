using Application;
using Domain.Entities;

namespace Infrastructure
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