using FlightPlanner.Exceptions;
using KellermanSoftware.CompareNetObjects;

namespace FlightPlanner.Helpers;

public static class ExtensionMethods
{
    public static void DoesFlightAlreadyExist(this IEnumerable<Flight> flights, Flight flight)
    {
        var doesFlightAlreadyExist = false;

        var compare = new CompareLogic();
        compare.Config.MembersToIgnore.Add("Id");

        foreach (var fl in flights)
        {
            if (compare.Compare(fl, flight).AreEqual) doesFlightAlreadyExist = true;
        }

        if (doesFlightAlreadyExist)
        {
            throw new FlightAlreadyExistException();
        }
    }

    public static bool IsFlightValid(this Flight flight)
    {
        var isNull = string.IsNullOrEmpty(flight.Carrier.Trim()) ||
                     string.IsNullOrEmpty(flight.DepartureTime.Trim()) ||
                     string.IsNullOrEmpty(flight.ArrivalTime.Trim()) ||
                     !flight.From.IsAirportValid() ||
                     !flight.To.IsAirportValid();

        var compare = new CompareLogic();

        if (!DateTime.TryParse(flight.DepartureTime, out var depart) ||
            !DateTime.TryParse(flight.ArrivalTime, out var arrive)) return false;

        if (depart >= arrive) return false;

        if (flight.From.Equals(flight.To)) return false;

        if (compare.Compare(flight.From, flight.To).AreEqual) return false;

        return !isNull;
    }

    private static bool IsAirportValid(this Airport? airport)
    {
        if (airport is null) return false;

        var isNull = string.IsNullOrEmpty(airport.City.Trim()) ||
                     string.IsNullOrEmpty(airport.Country.Trim()) ||
                     string.IsNullOrEmpty(airport.AirportCode.Trim());

        return !isNull;
    }

    public static bool AirportContainsKeyword(this Airport airport, string name)
    {
        return airport.City.ToLower().Contains(name.ToLower()) ||
               airport.Country.ToLower().Contains(name.ToLower()) ||
               airport.AirportCode.ToLower().Contains(name.ToLower());
    }

    public static bool DoesAirportAlreadyExists(this List<Airport> airports, Airport airport)
    {
        var compare = new CompareLogic
        {
            Config =
            {
                CaseSensitive = false
            }
        };

        return airports.Any(a => compare.Compare(a, airport).AreEqual);
    }
}
