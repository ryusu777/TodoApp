using IntegrationContext.Application.GiteaIssues.Queries.GetAssignmentsIssueNumber;
using IntegrationContext.Presentation.Core.Api;

namespace IntegrationContext.Presentation.GiteaIssues.Endpoint.GetAssignmentIssueNumber;

public record GetAssignmentsIssueNumberResponse(
    string? ErrorCode,
    string? ErrorDescription,
    GetAssignmentsIssueNumberResult? Data
) : IApiResponse<GetAssignmentsIssueNumberResult>;
