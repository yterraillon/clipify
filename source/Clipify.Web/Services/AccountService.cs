using Clipify.Domain.Entities;
using Clipify.Infrastructure.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clipify.Web.Services
{
    public class AccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client;
        }

        public async Task<User> GetUser()
        {
            // TODO: uri.
            return await _client.PostRequestAsync<User>(new Uri("..."), HttpMethod.Get);
        }
    }
}