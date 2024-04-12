using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Queries.GetProjectPages;

namespace ProjectManagement.Presentation.Project.Endpoints.GetProjectPages;

public class GetProjectPagesEndpoint : EndpointWithoutRequest<GetProjectPagesResponse>
{
    private readonly ISender _sender;

    public GetProjectPagesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(ProjectEndpointRoutes.GetProjectPages);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _sender.Send(new GetProjectPagesQuery(), ct);

        await SendAsync(new GetProjectPagesResponse(
            result.Value,
            null
        ));
    }
}
