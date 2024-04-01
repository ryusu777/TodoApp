using Library.Models;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Domain.Assignment;

public sealed class Assignment : AggregateRoot<AssignmentId>
{
    public Assignment(AssignmentId id) : base(id)
    {
    }

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required ProjectId ProjectId { get; set; }
    public required IEnumerable<SubdomainId> SubdomainIds { get; set; } = new List<SubdomainId>();
    public required IEnumerable<UserId> Assignees { get; set; }
}