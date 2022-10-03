namespace FlightPlanner.Helpers;

using Microsoft.EntityFrameworkCore;

public class FlightPlannerDbContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }

    protected readonly IConfiguration Configuration;

    public FlightPlannerDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Configuration.GetConnectionString("Flight-planner"));
    }
}
