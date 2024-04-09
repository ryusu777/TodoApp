using FastEndpoints;
using MediatR;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProject;

public class CreateProjectEndpoint : Endpoint<CreateProjectRequest, CreateProjectResponse>
{
    public ISender _sender;

    public CreateProjectEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(ProjectEndpointRoutes.Project);
    }

    public override async Task HandleAsync(CreateProjectRequest req, CancellationToken ct)
    {
        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateProjectResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
