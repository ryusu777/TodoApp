using IntegrationContext.Domain.GiteaIssues.ValueObjects;
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

    private GiteaRepository(
        GiteaRepositoryId id,
        ProjectId projectId,
        UserId owner,
        string repoName
    ) : base(id)
    {
        ProjectId = projectId;
        RepoOwner = owner;
        RepoName = repoName;
    }

    public static GiteaRepository Create(
        GiteaRepositoryId id,
        ProjectId projectId,
        UserId owner,
        string repoName
    )
    {
        return new(id, projectId, owner, repoName);
    }
}
