using finalProject_api.Dtos;
using finalProject_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardController(GameService gameService) : ControllerBase
    {
        [HttpGet("getLeaderboard")]
        public async Task<List<GameDto>> GetLeaderboard()
        {
            return await gameService.GetLeaderboard();
        }
    }
}
