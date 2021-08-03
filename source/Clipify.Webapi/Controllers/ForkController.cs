using Clipify.Application.Playlists.Commands.ForkPlaylist;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Playlists.Requests.GetForks;
using Clipify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipify.Webapi.Controllers
{
    public class ForkController : ApiController
    {
        // GET
        [HttpGet]
        public async Task<IEnumerable<ForkedPlaylist>> Get()
        {
            return await Mediator.Send(new GetForks.Request());
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistViewModel>> Post([FromBody] string name, string originalPlaylistId)
        {
            return await Mediator.Send(new ForkPlaylist.Request(name, originalPlaylistId));
        }
    }
}