using FlightPlaner.Data;
using FlightPlanner.Core.Exceptions;
using FlightPlanner.Core.Helpers;
using FlightPlanner.Core.Models;
using FlightPlanner.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner;

public static class FlightStorage
{
    private static readonly object _lockObject = new();

    // public static Flight AddFlight(Flight flight, FlightPlannerDbContext context)
    // {
    //     lock (_lockObject)
    //     {
    //         context.Flights
    //             .Include(f => f.To)
    //             .Include(f => f.From)
    //             .DoesFlightAlreadyExist(flight);
    //
    //         if (!flight.IsFlightValid()) throw new FlightIsNotValidException();
    //
    //         context.Flights.Add(flight);
    //         context.SaveChanges();
    //         return flight;
    //     }
    // }

    public static void Clear(FlightPlannerDbContext context)
    {
        lock (_lockObject)
        {
            context.Flights.RemoveRange(context.Flights);
            context.Airports.RemoveRange(context.Airports);
            context.SaveChanges();
        }
    }

    // public static Flight? GetFlightById(int id, FlightPlannerDbContext context)
    // {
    //     lock (_lockObject)
    //     {
    //         return context.Flights
    //             .Include(f => f.From)
    //             .Include(f => f.To)
    //             .FirstOrDefault(f => f.Id == id);
    //     }
    // }

    // public static Airport[] GetAirportsByKeyword(string keyword, FlightPlannerDbContext context)
    // {
    //     lock (_lockObject)
    //     {
    //         var airports = new List<Airport>();
    //
    //         var flIncludedAirports = context.Flights
    //             .Include(f => f.From)
    //             .Include(f => f.To);
    //
    //         foreach (var fl in flIncludedAirports)
    //         {
    //             if (!airports.DoesAirportAlreadyExists(fl.From))
    //                 airports.Add(fl.From);
    //             if (!airports.DoesAirportAlreadyExists(fl.To))
    //                 airports.Add(fl.To);
    //         }
    //
    //         return airports.Where(a => a.AirportContainsKeyword(keyword.Trim())).ToArray();
    //     }
    // }

    // public static void DeleteFlightById(int id, FlightPlannerDbContext context)
    // {
    //     lock (_lockObject)
    //     {
    //         var flight = GetFlightById(id, context);
    //
    //         if (flight is null) return;
    //
    //         context.Flights.Remove(flight);
    //         context.SaveChanges();
    //     }
    // }

    // public static PageResult SearchFlight(SearchFlightsRequest request, FlightPlannerDbContext context)
    // {
    //     lock (_lockObject)
    //     {
    //         var result = context.Flights.Where(f =>
    //             f.From.AirportCode.ToLower() == request.From.ToLower()
    //             &&
    //             f.To.AirportCode.ToLower() == request.To.ToLower()
    //             &&
    //             f.DepartureTime.Substring(0, 10) == request.DepartureDate
    //         );
    //
    //         return new PageResult
    //         {
    //             Items = result.ToArray(),
    //             Page = 0,
    //             TotalItems = result.Count()
    //         };
    //     }
    // }
}
