using Clipify.Application.Auth;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.SpotifyAuth.Hubs
{
    public class AuthHub : IAuthHub
    {
        public HubConnection Connection { get; }

        public AuthHub()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44389/spotify-auth-hub")
                .Build();

            Connection.Closed += Connection_Closed;
        }

        public async Task<string> GetConnectionId()
        {
            if (Connection.State != HubConnectionState.Connected)
                await Connection.StartAsync();

            return Connection.ConnectionId;
        }

        private async Task Connection_Closed(System.Exception arg)
        {
            await Task.Delay(5000);
            await Connection.StartAsync();
        }
    }
}