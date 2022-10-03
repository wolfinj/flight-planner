namespace FlightPlanner.Helpers;

using Microsoft.EntityFrameworkCore;

public class FlightPlannerDbContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }

    private readonly IConfiguration _configuration;

    public FlightPlannerDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetConnectionString("Flight-planner"));
    }
}
