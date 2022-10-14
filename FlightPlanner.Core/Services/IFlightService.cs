using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flight>
{
    Flight GetCompleteFlightById(int id);
    Flight AddFlight(Flight flight);
    Airport[] GetAirportsByKeyword(string keyword);
    PageResult SearchFlight(SearchFlightsRequest request);
}
