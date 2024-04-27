using AuthContext.Domain.User.Entities;
using Library.Models;

namespace AuthContext.Domain.User.Events;

public record RefreshTokenDeleted(UserRefreshToken UserRefreshToken) : IDomainEvent;
