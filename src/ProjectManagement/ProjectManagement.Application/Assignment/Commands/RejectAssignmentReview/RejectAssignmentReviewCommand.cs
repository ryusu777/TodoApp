using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.RejectAssignmentReview;

public record RejectAssignmentReviewCommand(
    Guid AssignmentId, 
    string RejectionNotes
) : ICommand;
