using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlightPlaner.Data;

public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<User> Users { get; set; }
    
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    private readonly IConfiguration _configuration;

    public FlightPlannerDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("Flight-planner"));
    }
}
