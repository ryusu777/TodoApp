using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.DeleteProject;

namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProject;

public class DeleteProjectEndpoint : Endpoint<DeleteProjectRequest, DeleteProjectResponse>
{
    public ISender _sender;

    public DeleteProjectEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete(ProjectEndpointRoutes.ProjectDetail);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(DeleteProjectRequest req, CancellationToken ct)
    {
        var result = await _sender.Send(new DeleteProjectCommand(req.id));
        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteProjectResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
