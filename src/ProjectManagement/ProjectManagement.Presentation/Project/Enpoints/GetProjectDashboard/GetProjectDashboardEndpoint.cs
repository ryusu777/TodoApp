using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Queries.GetProjectDashboard;

namespace ProjectManagement.Presentation.Project.Endpoints.GetProjectDashboard;

public class GetProjectDashboardEndpoint : EndpointWithoutRequest<GetProjectDashboardResponse>
{
    private readonly ISender _sender;

    public GetProjectDashboardEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(ProjectEndpointRoutes.Project);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _sender.Send(new GetProjectDashboardQuery(), ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults
                .BadRequest(new GetProjectDashboardResponse(
                    result.Error.Description, null)));

            return;
        }

        await SendOkAsync(new GetProjectDashboardResponse(null, result.Value));
    }
}
