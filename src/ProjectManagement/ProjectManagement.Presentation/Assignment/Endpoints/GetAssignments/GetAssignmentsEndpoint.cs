using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Queries.GetAssignments;

namespace ProjectManagement.Presentation.Assignment.Endpoints.GetAssignments;

public class GetAssignmentsEndpoint 
    : Endpoint<GetAssignmentsRequest, GetAssignmentsResponse>
{
    private readonly ISender _sender;

    public GetAssignmentsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(AssignmentEndpointRoutes.Assignments);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(GetAssignmentsRequest req, CancellationToken ct)
    {
        var result = await _sender
            .Send(new GetAssignmentsQuery(req.project_id), ct);
        
        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetAssignmentsResponse(result.Error.Description, null)));
            return;
        }

        await SendOkAsync(new GetAssignmentsResponse(
            null,
            result.Value
        ));
    }
}
