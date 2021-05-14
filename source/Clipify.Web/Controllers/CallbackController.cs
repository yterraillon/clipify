using Microsoft.AspNetCore.Mvc;

namespace Clipify.Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
