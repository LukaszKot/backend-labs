using OnTheTrail.TripService.Api.Database.Entities;

namespace OnTheTrail.TripService.Api.Repositories;

public interface IUserRepository
{
    Task<User?> Get(long id);
    void Add(User user);
    Task Save();
}