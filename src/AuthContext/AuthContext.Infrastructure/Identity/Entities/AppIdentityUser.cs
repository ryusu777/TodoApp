using AuthContext.Domain.User.Entities;
using AuthContext.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace AuthContext.Infrastructure.Identity.Entities;

public class AppIdentityUser : IdentityUser<Guid>
{
    public required List<UserRefreshToken> RefreshToken { get; set; }

    public GiteaUserId? GiteaUserId { get; set; }
}
