using ProjectManagement.Application.Subdomain.Commands.CreateSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomainKnowledge;

public record CreateSubdomainKnowledgeRequest : CreateSubdomainKnowledgeCommand
{
    public CreateSubdomainKnowledgeRequest(string Title, string Content, Guid SubdomainId) : base(Title, Content, SubdomainId)
    {
    }
}

