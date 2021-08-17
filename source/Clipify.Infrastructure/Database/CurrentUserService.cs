using AutoMapper;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;

namespace Clipify.Infrastructure.Database
{
    public class CurrentUserService : ICurrentUserService
    {
        private User User { get; }

        public CurrentUserService(IDbContext context, IMapper mapper)
        {
            User = mapper.Map<User>(context.Database
                .GetCollection<UserDto>()
                .FindOne(x => !string.IsNullOrEmpty(x.AccessToken)) ?? UserDto.Empty);
        }

        public User GetCurrentUser()
            => User;

        public bool IsUserLoggedIn()
            => !string.IsNullOrEmpty(User.Id) && !string.IsNullOrEmpty(User.AccessToken);
    }
}