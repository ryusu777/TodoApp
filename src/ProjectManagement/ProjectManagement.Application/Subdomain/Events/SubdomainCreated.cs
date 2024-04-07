using Library.Models;

namespace ProjectManagement.Application.Subdomain.Events;

public record SubdomainCreated(Domain.Subdomain.Subdomain Subdomain) : IDomainEvent;
