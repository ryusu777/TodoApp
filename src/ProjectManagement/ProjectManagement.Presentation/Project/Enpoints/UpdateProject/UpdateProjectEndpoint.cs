using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.UpdateProjectDetails;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProject;

public class UpdateProjectEndpoint : Endpoint<UpdateProjectRequest, UpdateProjectResponse>
{
    public ISender _sender;

    public UpdateProjectEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(ProjectEndpointRoutes.ProjectDetail);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateProjectRequest req, CancellationToken ct)
    {
        if (Route<string>("id") != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new UpdateProjectDetailsCommand(
                req.ProjectId,
                req.Name, 
                req.Description, 
                req.Status
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateProjectResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
