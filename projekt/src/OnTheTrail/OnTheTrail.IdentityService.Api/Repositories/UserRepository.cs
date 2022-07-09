using Microsoft.EntityFrameworkCore;
using OnTheTrail.IdentityService.Api.Database;
using OnTheTrail.IdentityService.Api.Database.Entities;

namespace OnTheTrail.IdentityService.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _dbContext;

    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetAsync(string email)
    {
        return _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public Task SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}