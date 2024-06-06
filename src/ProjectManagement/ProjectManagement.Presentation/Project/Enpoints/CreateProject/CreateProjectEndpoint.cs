using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.CreateProject;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProject;

public class CreateProjectEndpoint : Endpoint<CreateProjectCommand, CreateProjectResponse>
{
    public ISender _sender;

    public CreateProjectEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(ProjectEndpointRoutes.Project);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(CreateProjectCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateProjectResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
