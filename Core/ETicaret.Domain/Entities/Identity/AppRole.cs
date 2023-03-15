using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities.Identity;

// PK alanını <T> türünde belirtiyoruz.
public class AppRole : IdentityRole<Guid>
{
    
}