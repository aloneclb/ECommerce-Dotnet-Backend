using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string? NameSurname { get; set; }
}