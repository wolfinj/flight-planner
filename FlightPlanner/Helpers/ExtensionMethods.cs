using FlightPlanner.Exceptions;
using KellermanSoftware.CompareNetObjects;

namespace FlightPlanner.Helpers;

public static class ExtensionMethods
{
    public static void DoesFlightAlreadyExist(this List<Flight> flights, Flight flight)
    {
        var doesFlightAlreadyExist = false;

        CompareLogic compare = new CompareLogic();
        compare.Config.MembersToIgnore.Add("Id");

        flights.ForEach(f =>
        {
            if (compare.Compare(f, flight).AreEqual) doesFlightAlreadyExist = true;
        });

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

        CompareLogic compare = new CompareLogic();

        DateTime depart, arrive;

        if (!DateTime.TryParse(flight.DepartureTime, out depart) ||
            !DateTime.TryParse(flight.ArrivalTime, out arrive)) return false;

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
}
