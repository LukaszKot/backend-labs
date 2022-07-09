using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnTheTrail.TripService.Api.CQRS.Commands;
using OnTheTrail.TripService.Api.CQRS.Events;
using OnTheTrail.TripService.Api.Services;

namespace OnTheTrail.TripService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [Authorize]
    [HttpPost("search")]
    public Task<SearchStarted> Search(Search command)
    {
        var userId = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "id")?.Value!);
        return _tripService.Search(command, userId);
    }
}