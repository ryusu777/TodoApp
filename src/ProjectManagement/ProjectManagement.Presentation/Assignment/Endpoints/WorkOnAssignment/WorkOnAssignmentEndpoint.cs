using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.WorkOnAssignment;
using ProjectManagement.Presentation.Assignment;

namespace ProjectManagement.Presentation.Project.Endpoints.WorkOnAssignment;

public class WorkOnAssignmentEndpoint : Endpoint<WorkOnAssignmentCommand, WorkOnAssignmentResponse>
{
    private readonly ISender _sender;

    public WorkOnAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Patch(AssignmentEndpointRoutes.WorkOnAssignment);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(WorkOnAssignmentCommand req, CancellationToken ct)
    {
        if (req.AssignmentId != Route<Guid>("assignment_id")) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }
        var response = await _sender.Send(req, ct);
        if (response.IsFailure) 
        {
            await SendResultAsync(TypedResults
                .BadRequest(new WorkOnAssignmentResponse(response.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
