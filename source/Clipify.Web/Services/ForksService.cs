using Clipify.Application.Playlists.Commands.ForkPlaylist;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Extensions;
using Clipify.Web.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clipify.Web.Services
{
    public class ForksService
    {
        private readonly HttpClient _client;

        private readonly ApiSettings _apiSettings;
        
        public ForksService(HttpClient client, IOptions<ApiSettings> apiSettings)
        {
            _client = client;
            _apiSettings = apiSettings.Value;
        }

        public Task<IEnumerable<ForkedPlaylist>> GetForks()
        {
            return _client.PostRequestAsync<IEnumerable<ForkedPlaylist>>(new Uri($"{_apiSettings.BaseUrl}/Forks"), HttpMethod.Get);
        }
    }
}