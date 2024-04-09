using ProjectManagement.Application.Assignment.Commands.DeleteAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.DeleteAssignment;

public record DeleteAssignmentRequest : DeleteAssignmentCommand
{
    public DeleteAssignmentRequest(Guid AssignmentId) : base(AssignmentId)
    {
    }
}

