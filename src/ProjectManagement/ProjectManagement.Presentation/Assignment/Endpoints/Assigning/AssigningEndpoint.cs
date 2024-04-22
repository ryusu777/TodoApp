using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.Assigning;

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
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(AssigningRequest req, CancellationToken ct)
    {
        if (Route<Guid>("assignment_id") != req.AssignmentId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new AssigningCommand(
                req.AssignmentId, req.AssigneeUsername
            ), ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new AssigningResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
