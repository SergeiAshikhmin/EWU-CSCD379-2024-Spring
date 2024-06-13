using finalProject_api.Dtos;
using finalProject_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finalProject_api.Services;

public class GameService(AppDbContext Db)
{
    public async Task<List<GameDto>> GetLeaderboard()
    {
        var query = Db.Games
            .OrderByDescending(x => x.Score)
            .Take(10)
            .Select(x => new GameDto()
            {
                Name = x.Name,
                Lives = x.Lives,
                Score = x.Score,
                Time = x.Time
            });
        
        return await query.ToListAsync();
    }
}