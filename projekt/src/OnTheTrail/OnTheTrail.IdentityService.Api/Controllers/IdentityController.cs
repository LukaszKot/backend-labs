using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnTheTrail.IdentityService.Api.CQRS.Commands;
using OnTheTrail.IdentityService.Api.CQRS.Events;
using OnTheTrail.IdentityService.Api.Services;

namespace OnTheTrail.IdentityService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(Register command)
    {
        await _identityService.Register(command);
        return StatusCode((int) HttpStatusCode.Created);
    }
    
    [HttpPost("/login")]
    public async Task<LoggedIn> Login(Login command)
    {
        var loggedIn = await _identityService.Login(command);
        return loggedIn;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Test()
    {
        return Ok();
    }
}