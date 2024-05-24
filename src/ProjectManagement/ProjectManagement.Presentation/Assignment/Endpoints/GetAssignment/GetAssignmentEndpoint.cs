using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Queries.GetAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.GetAssignment;

public class GetAssignmentEndpoint 
    : Endpoint<GetAssignmentQuery, GetAssignmentResponse>
{
    private readonly ISender _sender;

    public GetAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(AssignmentEndpointRoutes.AssignmentDetail);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(GetAssignmentQuery req, CancellationToken ct)
    {
        var result = await _sender
            .Send(req);
        
        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetAssignmentResponse(result.Error.Description, null)));
            return;
        }

        await SendOkAsync(new GetAssignmentResponse(
            null,
            result.Value
        ));
    }
}
