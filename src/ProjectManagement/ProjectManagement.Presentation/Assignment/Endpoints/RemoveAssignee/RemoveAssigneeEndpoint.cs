using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands;

namespace ProjectManagement.Presentation.Assignment.Endpoints.RemoveAssignee;

public class RemoveAssigneeEndpoint : Endpoint<RemoveAssigneeRequest, RemoveAssigneeResponse>
{
    public ISender _sender;

    public RemoveAssigneeEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AssignmentEndpointRoutes.RemoveAssignee);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(RemoveAssigneeRequest req, CancellationToken ct)
    {
        if (Route<Guid>("assignment_id") != req.AssignmentId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new RemoveAssigneeCommand(
                req.AssignmentId, 
                req.AssigneeUsername
            ), ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new RemoveAssigneeResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
