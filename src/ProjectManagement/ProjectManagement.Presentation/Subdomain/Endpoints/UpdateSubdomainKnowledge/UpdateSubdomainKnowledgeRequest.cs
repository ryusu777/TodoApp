using ProjectManagement.Application.Subdomain.Commands.UpdateSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomainKnowledge;

public record UpdateSubdomainKnowledgeRequest : UpdateSubdomainKnowledgeCommand
{
    public required Guid knowledge_id { get; set; }

    public UpdateSubdomainKnowledgeRequest(Guid SubdomainKnowledgeId, string Title, string Content, Guid SubdomainId) : base(SubdomainKnowledgeId, Title, Content, SubdomainId)
    {
    }
}

