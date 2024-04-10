using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Subdomain.Entities;

public sealed class SubdomainKnowledge : Entity<SubdomainKnowledgeId>
{
#pragma warning disable CS8618
	private SubdomainKnowledge() { }
#pragma warning restore CS8618
	private SubdomainKnowledge(
        SubdomainKnowledgeId id,
        string title,
        string content,
        SubdomainId subdomainId
    ) : base(id)
    {
        Title = title;
        Content = content;
        SubdomainId = subdomainId;
    }

    public static SubdomainKnowledge Create(
        string title,
        string content,
        SubdomainId subdomainId
    ) {
        return new SubdomainKnowledge(
            SubdomainKnowledgeId.CreateUnique(),
            title,
            content,
            subdomainId
        );
    }
    
    public string Title { get; private set; }
    public string Content { get; private set; }
    public SubdomainId SubdomainId { get; private set; }

    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
    }
}
