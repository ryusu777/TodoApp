using ProjectManagement.Application.Subdomain.Commands.DeleteSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomainKnowledge;

public record DeleteSubdomainKnowledgeRequest : DeleteSubdomainKnowledgeCommand
{
    public DeleteSubdomainKnowledgeRequest(Guid SubdomainId, Guid SubdomainKnowledgeId) : base(SubdomainId, SubdomainKnowledgeId)
    {
    }
}

