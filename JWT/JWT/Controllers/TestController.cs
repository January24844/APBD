using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get")]
        // === Oznaczenie koncowki jako takiej, ktora wymaga podania tokenu (bycia "zalogowanym")
        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("exception")]
        public IActionResult ThrowException()
        {
            throw new Exception("No tragedia :(");
            return Ok();
        }
    }
}
