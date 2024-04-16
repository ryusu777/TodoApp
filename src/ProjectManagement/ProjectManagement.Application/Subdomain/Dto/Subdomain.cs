namespace ProjectManagement.Application.Subdomain.Dtos;

public record Subdomain(
    Guid Id,
    string Description,
    string Title,
    string ProjectId,
    ICollection<SubdomainKnowledge> Knowledges
) 
{
    public static Subdomain FromDomain(Domain.Subdomain.Subdomain subdomain)
    {
        return new(
            subdomain.Id.Value,
            subdomain.Description,
            subdomain.Title,
            subdomain.ProjectId.Value,
            subdomain.Knowledges
                .Select(e => SubdomainKnowledge.FromDomain(e))
                .ToList()
        );
    }
}

