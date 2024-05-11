using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.Entities;

public class RepositoryHook : Entity<RepositoryHookId>
{
#pragma warning disable CS8618
    private RepositoryHook() { }
#pragma warning restore CS8618

    public Uri HookUri { get; private set; }
    public ICollection<HookEvent> Events { get; private set; }
    public bool Active { get; private set; }

    private RepositoryHook(
        RepositoryHookId id,
        Uri hookUri,
        ICollection<HookEvent> events,
        bool active) : base(id)
    {
        HookUri = hookUri;
        Events = events;
        Active = active;
    }

    public static RepositoryHook Create(
        RepositoryHookId id,
        Uri hookUri,
        ICollection<HookEvent> events,
        bool active) 
    {
        return new(id, hookUri, events, active);
    }
}
