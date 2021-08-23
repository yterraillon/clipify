using System;
using MediatR;

namespace Events.Authentication
{
    public record LoggedInWithSpotify(string AccessToken, DateTime ExpirationDate) : INotification;
}