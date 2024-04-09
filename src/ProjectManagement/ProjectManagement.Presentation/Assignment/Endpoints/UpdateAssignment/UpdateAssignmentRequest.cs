using ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

namespace ProjectManagement.Presentation.Assignment.Endpoints.UpdateAssignment;

public record UpdateAssignmentRequest : UpdateAssignmentCommand
{
    public required Guid assignment_id { get; set; }
    public UpdateAssignmentRequest(Guid AssignmentId, string Title, string Description) : base(AssignmentId, Title, Description)
    {
    }
}

