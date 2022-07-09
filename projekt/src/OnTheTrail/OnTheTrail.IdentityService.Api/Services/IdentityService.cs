using System.Net;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using OnTheTrail.IdentityService.Api.CQRS.Commands;
using OnTheTrail.IdentityService.Api.CQRS.Events;
using OnTheTrail.IdentityService.Api.Database.Entities;
using OnTheTrail.IdentityService.Api.Exceptions;
using OnTheTrail.IdentityService.Api.Repositories;

namespace OnTheTrail.IdentityService.Api.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly IConfiguration _configuration;
    public IdentityService(IUserRepository userRepository, IHashingService hashingService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _configuration = configuration;
    }
    
    public async Task Register(Register register)
    {
        if (await _userRepository.GetAsync(register.Email) is not null)
        {
            throw new AppException("User with given email already exists.", HttpStatusCode.Conflict);
        }

        if (register.Password != register.RepeatPassword)
        {
            throw new AppException("Password and RepeatPassword does not match.");
        }

        var hash = _hashingService.Hash(register.Password);
        var user = User.Create(register.Email, hash);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
    }

    public async Task<LoggedIn> Login(Login login)
    {
        var user = await _userRepository.GetAsync(login.Email);
        if (user is null)
        {
            throw new AppException("Invalid credentials", HttpStatusCode.Unauthorized);
        }

        if (!_hashingService.Verify(login.Password, user.Hash))
        {
            throw new AppException("Invalid credentials", HttpStatusCode.Unauthorized);
        }

        var token = GenerateAccessToken(user);
        return new LoggedIn(token, DateTime.UtcNow.AddMinutes(30));
    }
    
    private string GenerateAccessToken(User user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("secretKey")))
            .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds())
            .AddClaim("email", user.Email)
            .AddClaim("id", user.Id)
            .Audience("audience")
            .Issuer("issuer")
            .Encode();
    }
}