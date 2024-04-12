using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.UpdateProjectPhases;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectPhases;

public class UpdateProjectPhasesEndpoint : Endpoint<UpdateProjectPhasesRequest, UpdateProjectPhasesResponse>
{
    public ISender _sender;

    public UpdateProjectPhasesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(ProjectEndpointRoutes.Phases);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateProjectPhasesRequest req, CancellationToken ct)
    {
        if (Route<string>("id") != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new UpdateProjectPhasesCommand(req.ProjectId, req.Phases), ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateProjectPhasesResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
