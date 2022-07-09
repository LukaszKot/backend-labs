namespace OnTheTrail.IdentityService.Api.CQRS.Events;

public record LoggedIn(string Token, DateTime ExpiresIn);