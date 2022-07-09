using OnTheTrail.TripService.Api.Enums;

namespace OnTheTrail.TripService.Api.Database.Entities;

public class Trip
{
    public long Id { get; protected set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public ICollection<TripNode> Path { get; set; } = new List<TripNode>();
    public Search? Query { get; set; }
    public User? User { get; set; }
    public long TripScore { get; set; }
}