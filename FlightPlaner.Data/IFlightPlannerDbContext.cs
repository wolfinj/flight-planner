using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightPlaner.Data;

public interface IFlightPlannerDbContext
{
    DbSet<Flight> Flights { get; set; }
    DbSet<Airport> Airports { get; set; }
    DbSet<User> Users { get; set; }

    DbSet<T> Set<T>() where T : class;
    EntityEntry<T> Entry<T>(T entity) where T : class;

    int SaveChanges();
    Task<int> SaveChangesAsync();
}
