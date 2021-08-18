namespace Application.User
{
    public interface ICurrentUserService
    {
        Domain.Entities.User GetCurrentUser();

        bool IsUserLoggedIn();
    }
}