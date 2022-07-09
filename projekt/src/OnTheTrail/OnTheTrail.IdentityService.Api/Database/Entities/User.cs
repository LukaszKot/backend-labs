using JetBrains.Annotations;
using OnTheTrail.IdentityService.Api.Exceptions;
using OnTheTrail.IdentityService.Api.Extensions;

namespace OnTheTrail.IdentityService.Api.Database.Entities;

public class User
{
    public long Id { get; private set; }
    public string Email { get; private set; }
    public string Hash { get; private set; }

    [UsedImplicitly]
    protected User() { }

    private User(string email, string hash)
    {
        Email = email;
        Hash = hash;
    }

    public void SetPassword(string hash)
    {
        Hash = hash;
    }

    public static User Create(string email, string hash)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new AppException($"{nameof(email)} has not been provided.");
        }
        if (!email.IsValidEmail())
        {
            throw new AppException($"{nameof(email)} is invalid.");
        }

        return new User(email, hash);
    }
}