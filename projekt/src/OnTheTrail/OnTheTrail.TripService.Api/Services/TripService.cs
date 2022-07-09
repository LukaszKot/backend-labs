using System.Net;
using OnTheTrail.TripService.Api.CQRS.Events;
using OnTheTrail.TripService.Api.Database.Entities;
using OnTheTrail.TripService.Api.Enums;
using OnTheTrail.TripService.Api.Exceptions;
using OnTheTrail.TripService.Api.Repositories;
using Search = OnTheTrail.TripService.Api.CQRS.Commands.Search;

namespace OnTheTrail.TripService.Api.Services;

public class TripService : ITripService
{
    private readonly IUserRepository _userRepository;
    private readonly ISearchRepository _searchRepository;

    public TripService(IUserRepository userRepository, ISearchRepository searchRepository)
    {
        _userRepository = userRepository;
        _searchRepository = searchRepository;
    }

    public async Task<SearchStarted> Search(Search search, long userId)
    {
        var user = await CreateOrGetUser(userId);
        if (user.Searches.Any(x => x.Status == SearchStatus.InProgress))
        {
            throw new AppException("User already has search in progress.", HttpStatusCode.Conflict);
        }

        var searchEntity = new Database.Entities.Search(user, search.MaxTripTimeInMinutes, search.MaxTrailDifficulty);
        var createdSearchEntity = await _searchRepository.AddAsync(searchEntity);
        await _userRepository.Save();
        return new SearchStarted(createdSearchEntity.Id);
    }

    private async Task<User> CreateOrGetUser(long userId)
    {
        var user = await _userRepository.Get(userId) ?? new User(userId);
        return user;
    }
}