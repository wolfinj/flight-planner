using FlightPlanner.Core.Interfaces;

namespace FlightPlanner.Core.Services;

public interface IServiceResult
{
    bool Success { get; }
    IEntity Entity { get; }
    IList<string> Errors { get; set; }
    string FormattedErrors { get; }
    ServiceResult SetEntity(IEntity entity);
    ServiceResult AddError(string error);
}
