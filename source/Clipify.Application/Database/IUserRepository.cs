namespace Clipify.Application.Database
{
    public interface IUserRepository
    {
        void CreateUser(string accessToken, string refreshToken, int expiresIn);
    }
}