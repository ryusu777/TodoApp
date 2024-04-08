using FastEndpoints;
using MediatR;

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
    }

    public override async Task HandleAsync(DeleteProjectRequest req, CancellationToken ct)
    {
        if (req.id != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender.Send(req);
        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteProjectResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
