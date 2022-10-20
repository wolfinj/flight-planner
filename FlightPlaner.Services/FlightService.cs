using FlightPlaner.Data;
using FlightPlanner.Core.Exceptions;
using FlightPlanner.Core.Helpers;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace FlightPlaner.Services;

public class FlightService : EntityService<Flight>, IFlightService
{
    public FlightService(FlightPlannerDbContext context) : base(context)
    {
    }

    public Flight GetCompleteFlightById(int id)
    {
        lock (Locker.LockObject)
        {
            return Context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }
    }

    public ServiceResult AddFlight(Flight flight)
    {
        lock (Locker.LockObject)
        {
            if (!flight.IsFlightValid()) throw new FlightIsNotValidException();

            if (DoesFlightAlreadyExist(flight)) throw new FlightAlreadyExistException();

            Context.Flights.Add(flight);
            Context.SaveChanges();
            return new ServiceResult(true).SetEntity(flight);
        }
    }

    public Airport[] GetAirportsByKeyword(string keyword)
    {
        lock (Locker.LockObject)
        {
            var distAirports = Context.Airports.Distinct().ToList();
            var distAirportsFilter = distAirports.Where(a =>
                    a.AirportContainsKeyword(keyword.Trim())
                )
                .ToArray();

            return distAirportsFilter;
        }
    }

    public PageResult SearchFlight(SearchFlightsRequest request)
    {
        lock (Locker.LockObject)
        {
            var result = Context.Flights.Where(f =>
                f.From.AirportCode.ToLower() == request.From.ToLower()
                &&
                f.To.AirportCode.ToLower() == request.To.ToLower()
                &&
                f.DepartureTime.Substring(0, 10) == request.DepartureDate
            );

            return new PageResult
            {
                Items = result.ToArray(),
                Page = 0,
                TotalItems = result.Count()
            };
        }
    }

    public bool DoesFlightAlreadyExist(Flight flight)
    {
        lock (Locker.LockObject)
        {
            var existingFlight = Context.Flights
                .Include(f => f.To)
                .Include(f => f.From)
                .Any(f =>
                    f.From.AirportCode == flight.From.AirportCode
                    &&
                    f.To.AirportCode == flight.To.AirportCode
                    &&
                    f.Carrier == flight.Carrier
                    &&
                    f.DepartureTime == flight.DepartureTime
                    &&
                    f.ArrivalTime == flight.ArrivalTime);

            return existingFlight;
        }
    }
}
