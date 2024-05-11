using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories.Entities;
using IntegrationContext.Domain.GiteaRepositories.Events;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories;

public class GiteaRepository : AggregateRoot<GiteaRepositoryId>
{
#pragma warning disable CS8618
    private GiteaRepository() { }
#pragma warning restore CS8618
    
    public ProjectId ProjectId { get; private set; }
    public UserId RepoOwner { get; private set; }
    public string RepoName { get; private set; }
    public ICollection<RepositoryHook> Hooks { get; private set; } = new List<RepositoryHook>();

    private GiteaRepository(
        GiteaRepositoryId id,
        ProjectId projectId,
        UserId owner,
        string repoName,
        ICollection<RepositoryHook>? hooks = null
    ) : base(id)
    {
        ProjectId = projectId;
        RepoOwner = owner;
        RepoName = repoName;

        if (hooks is not null)
            Hooks = hooks;
    }

    public static GiteaRepository Create(
        GiteaRepositoryId id,
        ProjectId projectId,
        UserId owner,
        string repoName,
        ICollection<RepositoryHook>? hooks = null
    )
    {
        return new(id, projectId, owner, repoName, hooks);
    }

    public void AddHook(RepositoryHook hook)
    {
        Hooks.Add(hook);

        RaiseDomainEvent(new RepositoryHookCreated(hook));
    }
}
