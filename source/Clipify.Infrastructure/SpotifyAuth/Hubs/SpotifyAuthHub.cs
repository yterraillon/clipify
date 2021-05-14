using System.Threading.Tasks;
using Clipify.Application.Auth;
using Microsoft.AspNetCore.SignalR;

namespace Clipify.Infrastructure.SpotifyAuth.Hubs
{
    public class SpotifyAuthHub : Hub<IAuthHub>
    {
        public Task SendAsync(string userId, string token) => 
            Clients.User(userId).Broadcast(token);

    }
}