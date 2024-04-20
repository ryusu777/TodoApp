namespace ProjectManagement.Application.Subdomain.Dtos;

public record SubdomainKnowledge(
    Guid Id,
    string Title,
    string Content
) 
{
    public static SubdomainKnowledge FromDomain(
        Domain.Subdomain.Entities.SubdomainKnowledge knowledge)
    {
        return new SubdomainKnowledge(knowledge.Id.Value, knowledge.Title, knowledge.Content);
    }
}

