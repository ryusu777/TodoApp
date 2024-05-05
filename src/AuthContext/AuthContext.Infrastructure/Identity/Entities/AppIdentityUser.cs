using AuthContext.Domain.User.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthContext.Infrastructure.Identity.Entities;

public class AppIdentityUser : IdentityUser<Guid>
{
    public List<UserRefreshToken> RefreshToken { get; set; } = new();
}
