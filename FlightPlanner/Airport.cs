using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightPlanner;

public class Airport : IEquatable<Airport>
{
    [Key, JsonIgnore] 
    public int Id { get; set; }
    
    public string Country { get; init; }

    public string City { get; init; }

    [JsonPropertyName("airport")] 
    public string AirportCode { get; init; }

    public bool Equals(Airport? other)
    {
        return string.Equals(Country.Trim(), other.Country.Trim(), StringComparison.OrdinalIgnoreCase) &&
               string.Equals(City.Trim(), other.City.Trim(), StringComparison.OrdinalIgnoreCase) &&
               string.Equals(AirportCode.Trim(), other.AirportCode.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
