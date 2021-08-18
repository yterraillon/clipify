namespace Application.SpotifyAuthentication.Requests.Login
{
    public interface IStateProvider
    {
        string State { get; }
    }
}