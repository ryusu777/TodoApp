using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.ApproveAssignmentReview;

public record ApproveAssignmentReviewResponse(string? ErrorDescription) : IApiResponse;
