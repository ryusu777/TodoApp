using AuthContext.Domain.User.ValueObjects;

namespace AuthContext.Domain.User.Events;

public record AccessTokenGranted(UserId Username);
