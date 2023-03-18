using ETicaret.Application.Abstractions.Services;
using ETicaret.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateAsync(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }
}