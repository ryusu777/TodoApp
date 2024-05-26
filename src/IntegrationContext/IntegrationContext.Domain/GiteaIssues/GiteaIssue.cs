using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues;

public class GiteaIssue : AggregateRoot<GiteaIssueId>
{
#pragma warning disable CS8618
    private GiteaIssue() { }
#pragma warning restore CS8618

    public AssignmentId AssignmentId { get; private set; }
    public GiteaRepositoryId GiteaRepositoryId { get; private set; }
    public IssueNumber IssueNumber { get; private set; }
    public string UpdatedAt { get; private set; }

    private GiteaIssue(
        GiteaIssueId id,
        IssueNumber issueNumber,
        AssignmentId assignmentId,
        GiteaRepositoryId giteaRepositoryId,
        string updateAt) : base(id)
    {
        AssignmentId = assignmentId;
        GiteaRepositoryId = giteaRepositoryId;
        IssueNumber = issueNumber;
        UpdatedAt = updateAt;
    }

    public static GiteaIssue Create(
        GiteaIssueId id,
        IssueNumber issueNumber,
        AssignmentId assignmentId,
        GiteaRepositoryId giteaRepositoryId,
        string updateAt)
    {
        return new(id, issueNumber, assignmentId, giteaRepositoryId, updateAt);
    }

    public void Update(string updatedAt)
    {
        UpdatedAt = updatedAt;
    }

    public void Update()
    {
        UpdatedAt = DateTime.Now.ToString();
    }
}
