using Microsoft.EntityFrameworkCore;
using OnTheTrail.TripService.Api.Database;
using OnTheTrail.TripService.Api.Database.Entities;

namespace OnTheTrail.TripService.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TripDbContext _dbContext;

    public UserRepository(TripDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> Get(long id)
    {
        return _dbContext.Users
            .Include(x => x.Searches)
            .Include(x => x.Trips)
            .ThenInclude(x => x.Path)
            .SingleOrDefaultAsync(x=> x.Id == id);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public Task Save()
    {
        return _dbContext.SaveChangesAsync();
    }
}