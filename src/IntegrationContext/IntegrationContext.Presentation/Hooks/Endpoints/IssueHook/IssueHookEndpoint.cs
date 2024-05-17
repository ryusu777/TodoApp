using FastEndpoints;
using IntegrationContext.Application.Hooks.Commands.HandleIssueCreate;
using MediatR;

namespace IntegrationContext.Presentation.Hooks.Endpoints.IssueHook;

public class IssueHookEndpoint : Endpoint<IssueHookRequest, object>
{
    public ISender _sender;

    public IssueHookEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(HooksRoutes.IssueHook);
        Group<HooksEndpointGroup>();
    }

    public override async Task HandleAsync(IssueHookRequest req, CancellationToken ct)
    {
        await _sender.Send(new HandleIssueCreateCommand(
            req.Issue.Id,
            req.IssueNumber,
            req.Issue.Title,
            req.Issue.Body,
            req.Issue.Assignees ?? new List<string>(),
            req.Issue.DueDate,
            req.Repository.Id
        ), ct);

        await SendNoContentAsync();
    }
}
