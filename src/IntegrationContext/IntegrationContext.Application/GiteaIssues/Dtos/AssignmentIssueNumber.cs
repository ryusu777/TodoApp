namespace IntegrationContext.Application.GiteaIssues.Dtos;

public record AssignmentIssueNumber(Guid AssignmentId, int IssueNumber, Uri IssueUrl);
