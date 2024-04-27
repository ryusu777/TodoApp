using AuthContext.Domain.User.Entities;
using Library.Models;

namespace AuthContext.Domain.User.Events;

public record RefreshTokenCreated(UserRefreshToken UserRefreshToken) : IDomainEvent;
