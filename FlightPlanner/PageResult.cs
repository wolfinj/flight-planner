using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightPlanner;

public class PageResult
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
    public Flight[] Items { get; set; }
}
