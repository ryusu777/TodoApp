using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

namespace ProjectManagement.Presentation.Assignment.Endpoints.UpdateAssignment;

public class UpdateAssignmentEndpoint : Endpoint<UpdateAssignmentCommand, UpdateAssignmentResponse>
{
    public ISender _sender;

    public UpdateAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(AssignmentEndpointRoutes.AssignmentDetail);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateAssignmentCommand req, CancellationToken ct)
    {
        if (Route<Guid>("assignment_id") != req.AssignmentId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateAssignmentResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
