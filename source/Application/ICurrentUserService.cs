using Domain.Entities;

namespace Application
{
    public interface ICurrentUserService
    {
        User GetCurrentUser();

        bool IsUserLoggedIn();
    }
}