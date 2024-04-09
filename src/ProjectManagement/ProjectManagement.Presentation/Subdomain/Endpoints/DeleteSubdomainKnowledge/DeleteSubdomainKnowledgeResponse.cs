using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomainKnowledge;

public record DeleteSubdomainKnowledgeResponse(string? ErrorDescription) : IApiResponse;
