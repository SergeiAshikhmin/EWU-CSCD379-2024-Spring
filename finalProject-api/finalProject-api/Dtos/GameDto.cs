namespace finalProject_api.Dtos;

public class GameDto
{
    public string Name { get; set; } = null!;
    public int Lives { get; set; }
    public int Score { get; set; }
    public DateTimeOffset Time { get; set; }
}