using ProjectManagement.Application.Assignment.Commands;

namespace ProjectManagement.Presentation.Assignment.Endpoints.RemoveAssignee;

public record RemoveAssigneeRequest : RemoveAssigneeCommand
{
    public required Guid assignment_id { get; set; }

    public RemoveAssigneeRequest(Guid AssignmentId, string AssigneeUsername) : base(AssignmentId, AssigneeUsername)
    {
    }
}

