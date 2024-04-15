namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomainKnowledge;

public record DeleteSubdomainKnowledgeRequest(
    Guid subdomain_id, Guid knowledge_id
);

