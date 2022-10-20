using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flight>
{
    Flight GetCompleteFlightById(int id);
    ServiceResult AddFlight(Flight flight);
    Airport[] GetAirportsByKeyword(string keyword);
    PageResult SearchFlight(SearchFlightsRequest request);
    bool DoesFlightAlreadyExist(Flight flight);
}
