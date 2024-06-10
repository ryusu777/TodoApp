using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.RequestAssignmentReview;
using ProjectManagement.Presentation.Assignment;

namespace ProjectManagement.Presentation.Project.Endpoints.RequestAssignmentReview;

public class RequestAssignmentReviewEndpoint : Endpoint<RequestAssignmentReviewCommand, RequestAssignmentReviewResponse>
{
    private readonly ISender _sender;

    public RequestAssignmentReviewEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Patch(AssignmentEndpointRoutes.RequestAssignmentReview);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(RequestAssignmentReviewCommand req, CancellationToken ct)
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
                .BadRequest(new RequestAssignmentReviewResponse(response.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
