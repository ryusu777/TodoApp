using FastEndpoints;
using MediatR;

namespace ProjectManagement.Presentation.Assignment.Endpoints.Assigning;

public class AssigningEndpoint : Endpoint<AssigningRequest, AssigningResponse>
{
    public ISender _sender;

    public AssigningEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AssignmentEndpointRoutes.Assigning);
    }

    public override async Task HandleAsync(AssigningRequest req, CancellationToken ct)
    {
        if (req.assignment_id != req.AssignmentId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new AssigningResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
