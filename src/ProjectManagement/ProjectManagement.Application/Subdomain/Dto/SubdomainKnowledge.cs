namespace ProjectManagement.Application.Subdomain.Dtos;

public record SubdomainKnowledge(
    string Title,
    string Content
) 
{
    public static SubdomainKnowledge FromDomain(
        Domain.Subdomain.Entities.SubdomainKnowledge knowledge)
    {
        return new SubdomainKnowledge(knowledge.Title, knowledge.Content);
    }
}

