using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.ChangeAssignmentStatus;

namespace ProjectManagement.Presentation.Assignment.Endpoints.ChangeAssignmentStatus;

public class ChangeAssignmentStatusEndpoint : Endpoint<ChangeAssignmentStatusRequest, ChangeAssignmentStatusResponse>
{
    public ISender _sender;

    public ChangeAssignmentStatusEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AssignmentEndpointRoutes.AssignmentStatus);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(ChangeAssignmentStatusRequest req, CancellationToken ct)
    {
        if (Route<Guid>("assignment_id") != req.AssignmentId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new ChangeAssignmentStatusCommand(
                req.AssignmentId,
                req.AssignmentStatus
            ), ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new ChangeAssignmentStatusResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
