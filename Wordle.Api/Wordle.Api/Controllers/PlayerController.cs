using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Models;
using Wordle.Api.Services;
using Wordle.Api.DTOS;

namespace Wordle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _service;
    public PlayerController(PlayerService service)
	{
		_service = service;
	}

    [HttpGet("Player")]
    public async Task<Player?> GetPlayer(string playerName = "Guest")
    {
        return await _service.GetPlayer(playerName) ?? null;
    }
    
    [HttpGet("TopPlayers")]
    public async Task<Player[]> GetTopPlayers(int numberOfPlayers = 10)
    {
        return await _service.GetTopPlayers(numberOfPlayers);
    }
    
    [HttpPost("AddPlayer")]
	public async Task<PlayerDTO> Post(PlayerDTO request)
	{
		await _service.AddPlayer(request);
		return request;
	}
}