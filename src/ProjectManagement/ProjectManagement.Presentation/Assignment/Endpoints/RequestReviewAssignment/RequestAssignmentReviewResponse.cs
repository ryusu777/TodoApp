using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.RequestAssignmentReview;

public record RequestAssignmentReviewResponse(string? ErrorDescription) : IApiResponse;
