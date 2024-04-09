using ProjectManagement.Application.Subdomain.Commands.CreateSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomainKnowledge;

public record CreateSubdomainKnowledgeRequest : CreateSubdomainKnowledgeCommand
{
    public required Guid subdomain_id { get; set; }
    public CreateSubdomainKnowledgeRequest(string Title, string Content, Guid SubdomainId) : base(Title, Content, SubdomainId)
    {
    }
}

