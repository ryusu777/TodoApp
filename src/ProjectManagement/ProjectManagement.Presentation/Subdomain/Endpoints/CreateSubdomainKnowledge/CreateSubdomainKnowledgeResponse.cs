using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomainKnowledge;

public record CreateSubdomainKnowledgeResponse(string? ErrorDescription) : IApiResponse;
