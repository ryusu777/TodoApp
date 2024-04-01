using Library.Models;

namespace ProjectManagement.Domain.Subdomain.Events;

public record SubdomainDeleted(Subdomain Subdomain) : IDomainEvent
{
}
