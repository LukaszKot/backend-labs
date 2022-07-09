namespace OnTheTrail.IdentityService.Api.CQRS.Commands;

public record Register(string Email, string Password, string RepeatPassword);