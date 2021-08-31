using Domain.Entities;

namespace Application.User
{
    public interface ICurrentUserService
    {
        UserProfile GetCurrentUser();

        bool IsUserLoggedIn();
        bool IsUserLoggedInWithSpotify();

        bool HasNoServiceConnected();
    }
}