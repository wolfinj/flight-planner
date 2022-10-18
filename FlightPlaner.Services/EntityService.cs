using FlightPlaner.Data;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlaner.Services;

public class EntityService<T> : DbService, IEntityService<T> where T: Entity 
{
    public EntityService(FlightPlannerDbContext context) : base(context)
    {
    }
    
    public  ServiceResult Create(T entity)
    {
        return Create<T>(entity);
    }

    public  ServiceResult Delete(T entity)
    {
        return Delete<T>(entity);
    }

    public  ServiceResult Update(T entity)
    {
        return Update<T>(entity);
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
