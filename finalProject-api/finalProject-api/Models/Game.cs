namespace finalProject_api.Models;

public class Game
{
    public int GameId { get; set; }
    public string Name { get; set; } = null!;
    public int Lives { get; set; }
    public int Score { get; set; }
    public int SecondsPlayed { get; set; }
}