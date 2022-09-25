using System.Text.Json.Serialization;

namespace FlightPlanner;

public class Airport : IEquatable<Airport>
{
    public string Country { get; set; }

    public string City { get; set; }

    [JsonPropertyName("airport")] public string AirportCode { get; set; }

    public bool Equals(Airport? other)
    {
        return string.Equals(Country.Trim(), other.Country.Trim(), StringComparison.OrdinalIgnoreCase) &&
               string.Equals(City.Trim(), other.City.Trim(), StringComparison.OrdinalIgnoreCase) &&
               string.Equals(AirportCode.Trim(), other.AirportCode.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
