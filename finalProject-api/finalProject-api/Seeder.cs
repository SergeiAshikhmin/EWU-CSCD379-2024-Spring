using finalProject_api.Models;

namespace finalProject_api;

public class Seeder
{
    // Fake data
    private static List<Game> _games = new List<Game>()
    {
        { new Game() { Name = "John", Lives = 2, Score = 100, SecondsPlayed = 50 } },
        { new Game() { Name = "Jane", Lives = 1, Score = 200, SecondsPlayed = 20 } },
        { new Game() { Name = "Jack", Lives = 3, Score = 500, SecondsPlayed = 100 } },
        { new Game() { Name = "Jill", Lives = 2, Score = 100, SecondsPlayed = 430 } },
    };
    
    public static async Task Seed(AppDbContext db)
    {
        if (db.Games.Any())
        {
            return;
        }

        await db.Games.AddRangeAsync(_games);
        await db.SaveChangesAsync();
    }
}