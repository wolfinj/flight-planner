using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlightPlaner.Data;

public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
{
    private readonly IConfiguration _configuration;

    public FlightPlannerDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<User> Users { get; set; }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("Flight-planner"));
    }
}
