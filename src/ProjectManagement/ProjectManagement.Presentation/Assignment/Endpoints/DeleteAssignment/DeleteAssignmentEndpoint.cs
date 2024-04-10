using FastEndpoints;
using MediatR;

namespace ProjectManagement.Presentation.Assignment.Endpoints.DeleteAssignment;

public class DeleteAssignmentEndpoint : Endpoint<DeleteAssignmentRequest, DeleteAssignmentResponse>
{
    public ISender _sender;

    public DeleteAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete(AssignmentEndpointRoutes.DeleteAssignment);
    }

    public override async Task HandleAsync(DeleteAssignmentRequest req, CancellationToken ct)
    {
        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteAssignmentResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
