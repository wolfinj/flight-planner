using FlightPlaner.Data;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlaner.Services;

public class EntityService<T> : DbService, IEntityService<T> where T: Entity 
{
    public EntityService(FlightPlannerDbContext context) : base(context)
    {
    }
    
    public void Create(T entity)
    {
        Create<T>(entity);
    }

    public void Delete(T entity)
    {
        Delete<T>(entity);
    }

    public void Update(T entity)
    {
        Update<T>(entity);
    }

    public List<T> GetAll()
    {
        return GetAll<T>();
    }

    public T? GetById(int id)
    {
        return GetById<T>(id);
    }

    public IQueryable<T> Query()
    {
        return Query<T>();
    }

    
}
