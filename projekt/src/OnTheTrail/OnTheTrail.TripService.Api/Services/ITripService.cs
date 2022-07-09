using OnTheTrail.TripService.Api.CQRS.Commands;
using OnTheTrail.TripService.Api.CQRS.Events;

namespace OnTheTrail.TripService.Api.Services;

public interface ITripService
{
    Task<SearchStarted> Search(Search search, long userId);
}