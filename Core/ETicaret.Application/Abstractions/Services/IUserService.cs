using ETicaret.Application.Dtos.Token;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Abstractions.Services;

public interface IUserService
{
    Task<bool> CreateAsync(AppUser user, string password);
}