using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Models;

public class Flight : Entity, IEquatable<Flight>
{
    [Key]
    public Airport From { get; set; }

    public Airport To { get; set; }
    public string Carrier { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }

    public bool Equals(Flight? other)
    {
        if (From != other.From) return false;
        if (To != other.To) return false;
        if (Carrier.Trim() != other.Carrier.Trim()) return false;
        if (DepartureTime.Trim() != other.DepartureTime.Trim()) return false;
        if (ArrivalTime.Trim() != other.ArrivalTime.Trim()) return false;

        return true;
    }
}
