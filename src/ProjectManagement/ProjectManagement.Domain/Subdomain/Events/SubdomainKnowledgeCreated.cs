using Library.Models;
using ProjectManagement.Domain.Subdomain.Entities;

namespace ProjectManagement.Domain.Subdomain.Events;

public record SubdomainKnowledgeCreated(SubdomainKnowledge SubdomainKnowledge) : IDomainEvent
{ }
