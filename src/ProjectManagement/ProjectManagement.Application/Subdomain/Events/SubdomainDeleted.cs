using Library.Models;

namespace ProjectManagement.Application.Subdomain.Events;

public record SubdomainDeleted(Domain.Subdomain.Subdomain Subdomain) : IDomainEvent;
