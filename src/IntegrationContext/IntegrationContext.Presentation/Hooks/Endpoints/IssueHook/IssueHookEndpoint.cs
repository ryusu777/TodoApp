using FastEndpoints;
using IntegrationContext.Application.Hooks.Commands.HandleIssueCreate;
using IntegrationContext.Application.Hooks.Commands.HandleIssueUpdate;
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
        if (req.Action == IssueHookRequest.IssueAction.Opened)
            await _sender.Send(new HandleIssueCreateCommand(
                req.Issue.Id,
                req.IssueNumber,
                req.Issue.Title,
                req.Issue.Body,
                req.Issue.Assignees
                    ?.Select(e => e.Username)
                    .ToList() ?? new List<string>(),
                req.Issue.DueDate,
                req.Repository.Id
            ), ct);

        if (req.Action == IssueHookRequest.IssueAction.Edited ||
            req.Action == IssueHookRequest.IssueAction.Assigned)
            await _sender.Send(new HandleIssueUpdateCommand(
                req.Issue.Id,
                req.Issue.Title,
                req.Issue.Body,
                req.Issue.Assignees
                    ?.Select(e => e.Username)
                    .ToList() ?? new List<string>(),
                req.Issue.UpdatedAt,
                req.Issue.DueDate
            ), ct);

        await SendNoContentAsync();
    }
}
