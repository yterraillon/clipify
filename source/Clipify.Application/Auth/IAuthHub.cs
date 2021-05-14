using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Clipify.Application.Auth
{
    public interface IAuthHub
    {
        HubConnection Connection { get; }

        Task<string> GetConnectionId();
    }
}