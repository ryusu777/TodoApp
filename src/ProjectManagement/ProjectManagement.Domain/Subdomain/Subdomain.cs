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
    }

    public static Subdomain Create(
        string description,
        string title,
        ProjectId projectId,
        ICollection<SubdomainKnowledge>? knowledges = null
    ) {
        var result = new Subdomain(
            SubdomainId.CreateUnique(),
            description,
            title,
            projectId,
            knowledges ?? new List<SubdomainKnowledge>()
        );

        return result;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public ProjectId ProjectId { get; private set; }
    public ICollection<SubdomainKnowledge> Knowledges { get; private set; }

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

    public Result UpdateKnowledge(
        SubdomainKnowledgeId knowledgeId, 
        string title,
        string content)
    {
        SubdomainKnowledge? found = Knowledges.FirstOrDefault(k => k.Id == knowledgeId);

        if (found is null)
        {
            return SubdomainDomainErrors.SubdomainKnowledgeNotFound;
        }

        found.Update(title, content);

        RaiseDomainEvent(new SubdomainKnowledgeUpdated(found));

        return Result.Success();
    }

    public Result DeleteKnowledge(SubdomainKnowledgeId id)
    {
        SubdomainKnowledge? found = Knowledges.FirstOrDefault(k => k.Id == id);

        if (found is null)
        {
            return SubdomainDomainErrors.SubdomainKnowledgeNotFound;
        }

        Knowledges.Remove(found);

        RaiseDomainEvent(new SubdomainKnowledgeDeleted(found));

        return Result.Success();
    }
}
