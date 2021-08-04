using Clipify.Application.Playlists.Commands.DeleteLocalPlaylist;
using Clipify.Application.Playlists.Commands.SavePlaylist;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Playlists.Requests.GetLocalPlaylists;
using Clipify.Application.Playlists.Requests.GetPlaylist;
using Clipify.Application.Playlists.Requests.GetPlaylists;
using Clipify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clipify.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ApiController
    {
        // GET: api/<PlaylistController>
        [HttpGet("api/LocalPlaylists")]
        public async Task<IActionResult> GetLocalPlaylists()
        {
            var response = await Mediator.Send(new GetLocalPlaylists.Request());

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetPlaylists.Request());

            return Ok(response);
        }

        // GET api/<PlaylistController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await Mediator.Send(new GetPlaylist.Request(id));

            return Ok(response);
        }

        // POST api/<PlaylistController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string playlistId, string snapshotId, string title)
        {
            await Mediator.Send(new SavePlaylist.Command(playlistId, snapshotId, title));

            return Ok();
        }

        // DELETE api/<PlaylistController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await Mediator.Send(new DeleteLocalPlaylist.Command(id));

            return Ok(response);
        }
    }
}
