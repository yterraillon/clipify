using Clipify.Application.Playlists.Models;
using Clipify.Application.Playlists.Requests.GetPlaylist;
using Clipify.Application.Playlists.Requests.GetPlaylists;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clipify.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ApiController
    {
        // GET: api/<PlaylistController>
        [HttpGet]
        public async Task<IEnumerable<PlaylistViewModel>> Get()
        {
            return await Mediator.Send(new GetPlaylists.Request());
        }

        // GET api/<PlaylistController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistViewModel>> Get(string id)
        {
            return await Mediator.Send(new GetPlaylist.Request(id));
        }

        // POST api/<PlaylistController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PlaylistController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlaylistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
