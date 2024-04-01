using Library.Models;

namespace ProjectManagement.Domain.Subdomain.Events;

public record SubdomainUpdated(Subdomain Subdomain) : IDomainEvent
{ }
