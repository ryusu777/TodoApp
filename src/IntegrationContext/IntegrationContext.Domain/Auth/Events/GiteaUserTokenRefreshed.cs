using Library.Models;

namespace IntegrationContext.Domain.Auth.Events;

public record GiteaUserTokenRefreshed(GiteaUser User) : IDomainEvent;
