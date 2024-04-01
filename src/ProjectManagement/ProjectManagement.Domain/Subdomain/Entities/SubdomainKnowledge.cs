using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Subdomain.Entities;

public sealed class SubdomainKnowledge : Entity<SubdomainKnowledgeId>
{
    private SubdomainKnowledge(
        SubdomainKnowledgeId id,
        string title,
        string content
    ) : base(id)
    {
        Title = title;
        Content = content;
    }

    public static SubdomainKnowledge Create(
        string title,
        string content
    ) {
        return new SubdomainKnowledge(
            SubdomainKnowledgeId.CreateUnique(),
            title,
            content
        );
    }
    
    public string Title { get; private set; }
    public string Content { get; private set; }

    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
    }
}