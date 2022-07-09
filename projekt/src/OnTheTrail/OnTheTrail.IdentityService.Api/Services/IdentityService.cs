using System.Net;
using OnTheTrail.IdentityService.Api.CQRS.Commands;
using OnTheTrail.IdentityService.Api.Database.Entities;
using OnTheTrail.IdentityService.Api.Exceptions;
using OnTheTrail.IdentityService.Api.Repositories;

namespace OnTheTrail.IdentityService.Api.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    public IdentityService(IUserRepository userRepository, IHashingService hashingService)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
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
}