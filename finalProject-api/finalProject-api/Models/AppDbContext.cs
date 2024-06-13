using Microsoft.EntityFrameworkCore;

namespace finalProject_api.Models;

public class AppDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}