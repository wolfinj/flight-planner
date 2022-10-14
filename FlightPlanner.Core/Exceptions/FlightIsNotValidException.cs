namespace FlightPlanner.Core.Exceptions;

public class FlightIsNotValidException : Exception
{
    public FlightIsNotValidException() : base("Flight is not valid.")
    {
    }

    public FlightIsNotValidException(string message)
        : base(message)
    {
    }
}
