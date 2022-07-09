using OnTheTrail.IdentityService.Api.Database.Entities;

namespace OnTheTrail.IdentityService.Api.Repositories;

public interface IUserRepository
{
    public Task<User?> GetAsync(string email);
    public Task AddAsync(User user);
    public Task SaveAsync();
}