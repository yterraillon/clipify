using Clipify.Domain.Entities;

namespace Clipify.Application.Users
{
    public interface ICurrentUserService
    {
        User GetCurrentUser();
    }
}