using ProjectManagement.Application.Subdomain.Commands.UpdateSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomainKnowledge;

public record UpdateSubdomainKnowledgeRequest : UpdateSubdomainKnowledgeCommand
{
    public UpdateSubdomainKnowledgeRequest(
        Guid SubdomainKnowledgeId, 
        string Title, 
        string Content, 
        Guid SubdomainId) : base(SubdomainKnowledgeId, Title, Content, SubdomainId)
    {
    }
}

