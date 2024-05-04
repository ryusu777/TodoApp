using Library.Models;

namespace IntegrationContext.Domain.Auth.Events;

public record GiteaUserCreated(GiteaUser User) : IDomainEvent;
