using Library.Models;
using ProjectManagement.Domain.Subdomain.Entities;

namespace ProjectManagement.Domain.Subdomain.Events;

public record SubdomainKnowledgeDeleted(SubdomainKnowledge SubdomainKnowledge) : IDomainEvent
{ }
