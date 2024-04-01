using Library.Models;
using ProjectManagement.Domain.Project.ValueObjects;
using ProjectManagement.Domain.Subdomain.Entities;
using ProjectManagement.Domain.Subdomain.Events;

namespace ProjectManagement.Domain.Subdomain;

public sealed class Subdomain : AggregateRoot<SubdomainId>
{
    private Subdomain(
        SubdomainId id,
        string description,
        string title,
        ProjectId projectId,
        ICollection<SubdomainKnowledge> knowledges
    ) : base(id)
    {
        Description = description;
        ProjectId = projectId;
        Knowledges = knowledges;
        Title = title;
        RaiseDomainEvent(new SubdomainCreated(this));
    }

    public static Subdomain Create(
        string description,
        string title,
        ProjectId projectId,
        ICollection<SubdomainKnowledge>? knowledges
    ) {
        return new Subdomain(
            SubdomainId.CreateUnique(),
            description,
            title,
            projectId,
            knowledges ?? new List<SubdomainKnowledge>()
        );
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public ProjectId ProjectId { get; private set; }
    public ICollection<SubdomainKnowledge> Knowledges { get; private set; }

    public void Delete()
    {
        RaiseDomainEvent(new SubdomainDeleted(this));
    }
    public void Update(string title, string description)
    {
        Title = title;
        Description = description;

        RaiseDomainEvent(new SubdomainUpdated(this));
    }

    public void AddKnowledge(SubdomainKnowledge knowledge)
    {
        Knowledges.Add(knowledge);

        RaiseDomainEvent(new SubdomainKnowledgeCreated(knowledge));
    }

    public Result UpdateKnowledge(SubdomainKnowledge knowledge)
    {
        SubdomainKnowledge? found = Knowledges.FirstOrDefault(k => k == knowledge);

        if (found is null)
        {
            return SubdomainDomainErrors.SubdomainKnowledgeNotFound;
        }

        found.Update(knowledge.Title, knowledge.Content);

        RaiseDomainEvent(new SubdomainKnowledgeUpdated(found));

        return Result.Success();
    }

    public Result DeleteKnowledge(SubdomainKnowledge knowledge)
    {
        SubdomainKnowledge? found = Knowledges.FirstOrDefault(k => k == knowledge);

        if (found is null)
        {
            return SubdomainDomainErrors.SubdomainKnowledgeNotFound;
        }

        Knowledges.Remove(found);

        RaiseDomainEvent(new SubdomainKnowledgeDeleted(found));

        return Result.Success();
    }
}