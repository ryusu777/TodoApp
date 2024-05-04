using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.Auth.Events;

public record AccessTokenGranted(
    JwtToken JwtToken, RefreshToken RefreshToken) : IDomainEvent;
