using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomainKnowledge;

public record UpdateSubdomainKnowledgeResponse(string? ErrorDescription) : IApiResponse;
