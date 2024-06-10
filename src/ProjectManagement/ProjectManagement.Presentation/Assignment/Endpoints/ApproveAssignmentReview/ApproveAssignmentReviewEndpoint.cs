using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.ApproveAssignmentReview;
using ProjectManagement.Presentation.Assignment;

namespace ProjectManagement.Presentation.Project.Endpoints.ApproveAssignmentReview;

public class ApproveAssignmentReviewEndpoint : Endpoint<ApproveAssignmentReviewCommand, ApproveAssignmentReviewResponse>
{
    private readonly ISender _sender;

    public ApproveAssignmentReviewEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Patch(AssignmentEndpointRoutes.ApproveAssignmentReview);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(ApproveAssignmentReviewCommand req, CancellationToken ct)
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
                .BadRequest(new ApproveAssignmentReviewResponse(response.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
