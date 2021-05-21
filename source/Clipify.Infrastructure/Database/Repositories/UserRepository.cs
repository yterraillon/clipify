using AutoMapper;
using Clipify.Application.Database;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;

namespace Clipify.Infrastructure.Database.Repositories
{
    public class UserRepository : Repository<User, UserDto, string>, IUserRepository
    {
        public UserRepository(IMapper mapper, IDbContext context) : base(mapper, context)
        {
        }

        public void CreateUser(string accessToken, string refreshToken, int expiresIn)
        {
            Add(new UserDto
            {
                AccessToken = accessToken,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken
            });
        }
    }
    
}