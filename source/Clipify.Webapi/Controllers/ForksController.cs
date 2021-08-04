using Clipify.Application.Playlists.Commands.ForkPlaylist;
using Clipify.Application.Playlists.Requests.GetForks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clipify.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForksController : ApiController
    {
        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response =  await Mediator.Send(new GetForks.Request());

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name, string originalPlaylistId)
        {
            var response = await Mediator.Send(new ForkPlaylist.Request(name, originalPlaylistId));

            return Ok(response);
        }
    }
}