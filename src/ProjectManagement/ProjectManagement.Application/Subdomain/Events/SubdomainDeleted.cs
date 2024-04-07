using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Subdomain.Events;

public record SubdomainDeleted(SubdomainId SubdomainId) : IDomainEvent;
