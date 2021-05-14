﻿using Clipify.Application;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.SpotifyAuth.Hubs
{
    public class SpotifyAuthHub : Hub<ISignalRHub>
    {
        public Task SendAsync(string userId, string token) =>
            Clients.User(userId).Broadcast(token);
    }
}