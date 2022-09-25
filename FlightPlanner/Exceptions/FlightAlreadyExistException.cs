namespace FlightPlanner.Exceptions;

public class FlightAlreadyExistException : Exception
{
    public FlightAlreadyExistException() :base("Flight already exists")
    {
    }

    public FlightAlreadyExistException(string message)
        : base(message)
    {
    }

}
