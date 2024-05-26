using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.GiteaIssues.Queries.GetAssignmentsIssueNumber;

public record GetAssignmentsIssueNumberQuery(ICollection<Guid> AssignmentIds) : IQuery<GetAssignmentsIssueNumberResult>;
