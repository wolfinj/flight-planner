using FlightPlanner.Exceptions;
using FlightPlanner.Helpers;

namespace FlightPlanner;

public class FlightStorage
{
    private static List<Flight> _flights = new();
    private static int _id;

    public static Flight AddFlight(Flight flight)
    {
        _flights.DoesFlightAlreadyExist(flight);
        
        if (!flight.IsFlightValid()) throw new FlightIsNotValidException();

        flight.Id = _id++;
        _flights.Add(flight);
        return flight;
    }

    public static void Clear()
    {
        _flights.Clear();
        _id = 0;
    }

    public static Flight? GetFlightById(int id)
    {
        return _flights.FirstOrDefault(f => f.Id == id);
    }

    public static bool DeleteFlightById(int id)
    {
        Flight? flight = GetFlightById(id);

        if (flight is null) return false;

        _flights.Remove(flight);
        
        return true;
    }
}
