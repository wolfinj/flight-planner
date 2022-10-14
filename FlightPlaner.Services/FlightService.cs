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
        return _context.Flights
            .Include(f => f.From)
            .Include(f => f.To)
            .SingleOrDefault(f => f.Id == id);
    }

    public Flight AddFlight(Flight flight)
    {
        _context.Flights
            .Include(f => f.To)
            .Include(f => f.From)
            .DoesFlightAlreadyExist(flight);

        if (!flight.IsFlightValid()) throw new FlightIsNotValidException();

        _context.Flights.Add(flight);
        _context.SaveChanges();
        return flight;
    }
    
    public  Airport[] GetAirportsByKeyword(string keyword)
    {
            var airports = new List<Airport>();

            var flIncludedAirports = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To);

            foreach (var fl in flIncludedAirports)
            {
                if (!airports.DoesAirportAlreadyExists(fl.From))
                    airports.Add(fl.From);
                if (!airports.DoesAirportAlreadyExists(fl.To))
                    airports.Add(fl.To);
            }

            return airports.Where(a => a.AirportContainsKeyword(keyword.Trim())).ToArray();
    }
    
    public  PageResult SearchFlight(SearchFlightsRequest request)
    {
            var result = _context.Flights.Where(f =>
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
