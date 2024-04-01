using Library.Models;

namespace ProjectManagement.Domain.Subdomain;

public static class SubdomainDomainErrors
{
    public static Error SubdomainKnowledgeNotFound => new(nameof(SubdomainKnowledgeNotFound), "The knowledge does not found");
}
