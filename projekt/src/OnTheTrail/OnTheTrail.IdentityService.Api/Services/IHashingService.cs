namespace OnTheTrail.IdentityService.Api.Services;

public interface IHashingService
{
    string Hash(string password);
    bool Verify(string password, string hash);
}