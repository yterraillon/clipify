﻿using System;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.AuthorizeRequest;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Clipify.Infrastructure.SpotifyAuth.Hubs
{
    public class AuthHub
    {
        public HubConnection Connection { get; }

        private readonly IMediator _mediator;

        public AuthHub(IMediator mediator)
        {
            _mediator = mediator;
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