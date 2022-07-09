using OnTheTrail.IdentityService.Api.CQRS.Commands;

namespace OnTheTrail.IdentityService.Api.Services;

public interface IIdentityService
{
    Task Register(Register register);
}