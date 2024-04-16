namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomain;

public record GetSubdomainResult(
    Guid Id,
    string Description,
    string Title,
    string ProjectId,
    ICollection<Subdomain.Dtos.SubdomainKnowledge> Knowledges
);
