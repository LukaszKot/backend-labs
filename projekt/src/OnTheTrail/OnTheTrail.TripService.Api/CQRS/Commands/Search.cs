using OnTheTrail.TripService.Api.Enums;

namespace OnTheTrail.TripService.Api.CQRS.Commands;

public record Search(int MaxTripTimeInMinutes, TrailDifficulty MaxTrailDifficulty);