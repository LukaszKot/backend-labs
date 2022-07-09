using JetBrains.Annotations;

namespace OnTheTrail.TripService.Api.Database.Entities;

public class User
{
    public long Id { get; protected set;  }
    public ICollection<Search> Searches { get; } = new List<Search>();
    public ICollection<Trip> Trips { get; } = new List<Trip>();

    [UsedImplicitly]
    public User(){}
    
    public User(long id)
    {
        Id = id;
    }
}