using System;
using System.Linq;
using Application.SpotifyAuthentication.Requests.Login;

namespace Infrastructure.Spotify.Authentication
{
    public class StateProvider : IStateProvider
    {
        public string State { get; private set; } = string.Empty;

        private static readonly char[] Characters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
        };

        public StateProvider() => BuildState();

        private void BuildState()
        {
            const int length = 10;
            var random = new Random();
            State = new string(Enumerable.Repeat(Characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}