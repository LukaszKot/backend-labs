using JetBrains.Annotations;
using OnTheTrail.TripService.Api.Enums;

namespace OnTheTrail.TripService.Api.Database.Entities;

public class Search
{
    public long Id { get; protected set; }
    public User User { get; protected set; }
    public SearchStatus Status { get; protected set; }
    public int TripTimeInMinutes { get; protected set; }
    public TrailDifficulty TrailDifficulty { get; protected set; }
    public ICollection<Trip> Results { get; protected set; } = new List<Trip>();

    public Search(User user, int tripTimeInMinutes, TrailDifficulty trailDifficulty)
    {
        User = user;
        Status = SearchStatus.InProgress;
        TrailDifficulty = trailDifficulty;
        TripTimeInMinutes = tripTimeInMinutes;
    }
    
    [UsedImplicitly]
    protected Search(){}
}