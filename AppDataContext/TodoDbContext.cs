using LearnDotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LearnDotnet.AppDataContext;

public class TodoDbContext: DbContext
{
    private readonly DbSettings _dbSettings;

    public TodoDbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    // DbSet property to represent the Todo table
    public DbSet<Todo> Todos { get; set; }

    // Configuring the database provider and connection string
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_dbSettings.ConnectionString);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    // Configuring the model for the Todo entity
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().ToTable("todos").HasKey(x => x.Id);
    }
}