namespace Clipify.Application.Auth.Requests
{
    public interface IAuthCodeProvider
    {
        string Verifier { get; }

        string Challenge { get; }
    }
}