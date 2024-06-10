using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.RejectAssignmentReview;
using ProjectManagement.Presentation.Assignment;

namespace ProjectManagement.Presentation.Project.Endpoints.RejectAssignmentReview;

public class RejectAssignmentReviewEndpoint : Endpoint<RejectAssignmentReviewCommand, RejectAssignmentReviewResponse>
{
    private readonly ISender _sender;

    public RejectAssignmentReviewEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Patch(AssignmentEndpointRoutes.RejectAssignmentReview);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(RejectAssignmentReviewCommand req, CancellationToken ct)
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
                .BadRequest(new RejectAssignmentReviewResponse(response.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
