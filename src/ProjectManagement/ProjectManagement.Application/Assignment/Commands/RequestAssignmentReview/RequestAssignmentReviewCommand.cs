using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.RequestAssignmentReview;

public record RequestAssignmentReviewCommand(
    Guid AssignmentId,
    string Description
) : ICommand;
