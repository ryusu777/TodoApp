namespace ProjectManagement.Presentation.Subdomain;

public static class SubdomainEndpointRoutes
{
    public const string Subdomain = "/project/{project_id}/subdomain";
    public const string SubdomainDetail = "/subdomain/{subdomain_id}";
    public const string SubdomainKnowledgeDetail = "/subdomain-knowledge/{knowledge_id}";
    public const string DeleteSubdomainKnowledge = "/subdomain/{SubdomainId}/knowledge/{SubdomainKnowledgeId}";
}
