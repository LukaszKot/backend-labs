namespace OnTheTrail.TripService.Api.Database.Entities;

public class TripNode
{
    public long Id { get; protected set; }
    public int SequenceNumber { get; protected set; }
    public string? NextTrailColor { get; protected set; }
    public int? NextTrailTimeInMinutes { get; set; }
    public string? NextTrailDifficulty { get; set; }
    public int Height { get; set; }
}