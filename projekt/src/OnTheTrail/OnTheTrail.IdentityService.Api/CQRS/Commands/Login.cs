namespace OnTheTrail.IdentityService.Api.CQRS.Commands;

public record Login(string Email, string Password);