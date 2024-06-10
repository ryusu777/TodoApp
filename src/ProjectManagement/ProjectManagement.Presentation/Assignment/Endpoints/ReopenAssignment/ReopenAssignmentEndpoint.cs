using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.ReopenAssignment;
using ProjectManagement.Presentation.Assignment;

namespace ProjectManagement.Presentation.Project.Endpoints.ReopenAssignment;

public class ReopenAssignmentEndpoint : Endpoint<ReopenAssignmentCommand, ReopenAssignmentResponse>
{
    private readonly ISender _sender;

    public ReopenAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Patch(AssignmentEndpointRoutes.ReopenAssignment);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(ReopenAssignmentCommand req, CancellationToken ct)
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
                .BadRequest(new ReopenAssignmentResponse(response.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
