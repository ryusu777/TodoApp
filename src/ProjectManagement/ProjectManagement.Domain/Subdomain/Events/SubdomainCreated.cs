using Library.Models;

namespace ProjectManagement.Domain.Subdomain.Events;

public record SubdomainCreated(Subdomain subdomain) : IDomainEvent
{ }
