namespace Application.SpotifyAuthentication.Commands.Login
{
    public interface IStateProvider
    {
        string State { get; }
    }
}