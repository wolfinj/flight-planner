using FlightPlanner.Exceptions;
using FlightPlanner.Helpers;

namespace FlightPlanner;

public static class FlightStorage
{
    private static readonly List<Flight> _flights = new();
    private static readonly object _lockObject = new();
    private static int _id;

    public static Flight AddFlight(Flight flight)
    {
        lock (_lockObject)
        {
            _flights.DoesFlightAlreadyExist(flight);

            if (!flight.IsFlightValid()) throw new FlightIsNotValidException();

            flight.Id = _id++;
            _flights.Add(flight);
            return flight;
        }
    }

    public static void Clear()
    {
        lock (_lockObject)
        {
            _flights.Clear();
            _id = 0;
        }
    }

    public static Flight? GetFlightById(int id)
    {
        lock (_lockObject)
        {
            return _flights.FirstOrDefault(f => f.Id == id);
        }
    }

    public static Airport[] GetAirportsByKeyword(string keyword)
    {
        lock (_lockObject)
        {
            List<Airport> airports = new List<Airport>();

            _flights.ForEach(fl =>
            {
                if (!airports.DoesAirportAlreadyExists(fl.From))
                    airports.Add(fl.From);
                if (!airports.DoesAirportAlreadyExists(fl.To))
                    airports.Add(fl.To);
            });

            return airports.Where(a => a.AirportContainsKeyword(keyword.Trim())).ToArray();
        }
    }

    public static void DeleteFlightById(int id)
    {
        lock (_lockObject)
        {
            Flight? flight = GetFlightById(id);

            if (flight is null) return;

            _flights.Remove(flight);
        }
    }

    public static PageResult SearchFlight(SearchFlightsRequest request)
    {
        lock (_lockObject)
        {
            var result = _flights.Where(f =>
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
}
