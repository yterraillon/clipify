using Clipify.Domain.Entities;
using Clipify.Infrastructure.Extensions;
using Clipify.Web.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clipify.Web.Services
{
    public class AccountsService
    {
        private readonly HttpClient _client;

        private readonly ApiSettings _apiSettings;
        
        public AccountsService(HttpClient client, IOptions<ApiSettings> apiSettings)
        {
            _client = client;
            _apiSettings = apiSettings.Value;
        }

        public async Task<User> GetUser()
        {
            // TODO: uri.
            return await _client.PostRequestAsync<User>(new Uri($"{_apiSettings.BaseUrl}/Accounts"), HttpMethod.Get);
        }
    }
}