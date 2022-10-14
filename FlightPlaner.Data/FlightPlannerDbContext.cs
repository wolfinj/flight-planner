using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlightPlaner.Data;

public class FlightPlannerDbContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }

    private readonly IConfiguration _configuration;

    public FlightPlannerDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // public FlightPlannerDbContext(DbContextOptions options) : base(options)
    // {
    //     
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // options.UseSqlite(_configuration.GetConnectionString("Flight-planner"));
        options.UseSqlServer(_configuration.GetConnectionString("Flight-planner"));
    }
}
