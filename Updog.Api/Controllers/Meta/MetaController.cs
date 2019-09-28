using Microsoft.AspNetCore.Mvc;

namespace Updog.Api {
    [Route("api/meta")]
    [ApiController]
    public sealed class MetaController : ApiController {
        [HttpGet("alive")]
        public ActionResult Test() {
            return Ok("All good!");
        }
    }
}