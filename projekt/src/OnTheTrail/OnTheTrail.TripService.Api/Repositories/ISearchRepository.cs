using OnTheTrail.TripService.Api.Database.Entities;

namespace OnTheTrail.TripService.Api.Repositories;

public interface ISearchRepository
{
    Task<Search> AddAsync(Search search);
}