using System.ComponentModel.DataAnnotations;
using FlightPlanner.Core.Interfaces;

namespace FlightPlanner.Core.Models;

public abstract class Entity : IEntity
{
    [Key]
    public virtual int Id { get; set; }
}
