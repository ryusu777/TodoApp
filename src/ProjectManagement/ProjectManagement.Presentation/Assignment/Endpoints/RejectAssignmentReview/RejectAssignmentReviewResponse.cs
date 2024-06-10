using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.RejectAssignmentReview;

public record RejectAssignmentReviewResponse(string? ErrorDescription) : IApiResponse;
