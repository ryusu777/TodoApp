using ProjectManagement.Application.Assignment.Commands.Assigning;

namespace ProjectManagement.Presentation.Assignment.Endpoints.Assigning;

public record AssigningRequest : AssigningCommand
{
    public AssigningRequest(
        Guid AssignmentId, 
        string AssigneeUsername) : base(AssignmentId, AssigneeUsername)
    {
    }
}

