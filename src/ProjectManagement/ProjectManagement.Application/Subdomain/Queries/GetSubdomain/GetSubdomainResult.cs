using ProjectManagement.Application.Subdomain.Dtos;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomain;

public record GetSubdomainResult(
    Guid Id, string Description, string Title, string ProjectId, ICollection<SubdomainKnowledge> Knowledges
) : Dtos.Subdomain(Id, Description, Title, ProjectId, Knowledges);
