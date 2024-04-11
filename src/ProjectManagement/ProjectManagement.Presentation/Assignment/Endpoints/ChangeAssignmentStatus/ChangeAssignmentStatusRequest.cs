using ProjectManagement.Application.Assignment.Commands.ChangeAssignmentStatus;

namespace ProjectManagement.Presentation.Assignment.Endpoints.ChangeAssignmentStatus;

public record ChangeAssignmentStatusRequest : ChangeAssignmentStatusCommand
{
    public ChangeAssignmentStatusRequest(Guid AssignmentId, string AssignmentStatus) : base(AssignmentId, AssignmentStatus)
    {
    }
}

