using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Example of a DbSet for a model
    public DbSet<SensorData> SensorData { get; set; }

    // Optionally configure the connection in OnConfiguring
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // You can also specify connection string here, but dependency injection is preferred
        // optionsBuilder.UseSqlServer("your_connection_string");
    }

    // Optionally use Fluent API for additional configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add any specific model configurations
        base.OnModelCreating(modelBuilder);
    }
}