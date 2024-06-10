using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.ApproveAssignmentReview;

public record ApproveAssignmentReviewCommand(
    Guid AssignmentId,
    string UserId
) : ICommand;
