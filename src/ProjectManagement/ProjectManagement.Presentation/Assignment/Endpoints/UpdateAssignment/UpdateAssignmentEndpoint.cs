using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

namespace ProjectManagement.Presentation.Assignment.Endpoints.UpdateAssignment;

public class UpdateAssignmentEndpoint : Endpoint<UpdateAssignmentRequest, UpdateAssignmentResponse>
{
    public ISender _sender;

    public UpdateAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(AssignmentEndpointRoutes.AssignmentDetail);
    }

    public override async Task HandleAsync(UpdateAssignmentRequest req, CancellationToken ct)
    {
        if (req.assignment_id != req.AssignmentId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new UpdateAssignmentCommand(
                req.AssignmentId, 
                req.Title, 
                req.Description
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateAssignmentResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}