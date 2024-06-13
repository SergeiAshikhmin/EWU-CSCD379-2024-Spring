using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        [HttpGet("getLeaderboard")]
        public IActionResult GetLeaderboard()
        {
            return Ok("Here is my message!");
        }
    }
}
