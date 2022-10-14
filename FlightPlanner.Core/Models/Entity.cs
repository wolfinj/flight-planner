using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Models;

public class Entity
{
    [Key]
    public virtual int Id { get; set; }
}
