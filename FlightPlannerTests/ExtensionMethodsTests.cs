using FlightPlanner;
using FlightPlanner.Helpers;

namespace FlightPlannerTests;


public class ExtensionMethodsTests
{
    [Fact]
    public void AirportContainsKeyword_CheckIfAirportContainsKey_ExpectToBeTrue()
    {
        Airport rix = new Airport
        {
            Country = "Lat",
            City = "Rig",
            AirportCode = "RIX"
        };

        var act = rix.AirportContainsKeyword("rix");

        act.Should().BeTrue();
    }
}
