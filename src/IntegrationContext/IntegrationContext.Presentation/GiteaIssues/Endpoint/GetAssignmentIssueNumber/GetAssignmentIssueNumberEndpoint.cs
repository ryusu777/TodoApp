using FastEndpoints;
using IntegrationContext.Application.GiteaIssues.Queries.GetAssignmentsIssueNumber;
using MediatR;

namespace IntegrationContext.Presentation.GiteaIssues.Endpoint.GetAssignmentIssueNumber;

public class GetAssignmentIssueNumberEndpoint : Endpoint<GetAssignmentsIssueNumberQuery, GetAssignmentsIssueNumberResponse>
{
    public ISender _sender;

    public GetAssignmentIssueNumberEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(GiteaIssuesRoutes.AssignmentIssueNumber);
        Group<GiteaIssuesEndpointGroup>();
    }

    public override async Task HandleAsync(GetAssignmentsIssueNumberQuery req, CancellationToken ct)
    {
        var userId = User
            .Claims
            .FirstOrDefault(e => e.Type == "sub")
            ?.Value;

        if (userId is null)
        {
            await SendForbiddenAsync();
            return;
        }

        var result = await _sender.Send(req, ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetAssignmentsIssueNumberResponse(result.Error.Code, result.Error.Description, null)));
            return;
        }

        await SendOkAsync(
            new GetAssignmentsIssueNumberResponse(null, null, result.Value));
    }
}
