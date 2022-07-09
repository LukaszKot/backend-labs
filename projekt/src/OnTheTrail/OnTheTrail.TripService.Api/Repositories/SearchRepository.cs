using OnTheTrail.TripService.Api.Database;
using OnTheTrail.TripService.Api.Database.Entities;

namespace OnTheTrail.TripService.Api.Repositories;

public class SearchRepository : ISearchRepository
{
    private readonly TripDbContext _dbContext;

    public SearchRepository(TripDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Search> AddAsync(Search search)
    {
        var result = await _dbContext.Searches.AddAsync(search);
        return result.Entity;
    }
}