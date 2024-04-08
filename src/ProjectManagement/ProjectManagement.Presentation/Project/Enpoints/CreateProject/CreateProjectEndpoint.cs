using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.CreateProject;

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
        CreateProjectCommand command = new CreateProjectCommand(
            req.Code,
            req.Name,
            req.Description,
            req.ProjectMembers,
            req.ProjectPhases
        );

        var result = await _sender.Send(command);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateProjectResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
