using OnTheTrail.IdentityService.Api.CQRS.Commands;
using OnTheTrail.IdentityService.Api.CQRS.Events;

namespace OnTheTrail.IdentityService.Api.Services;

public interface IIdentityService
{
    Task Register(Register register);
    Task<LoggedIn> Login(Login login);
}