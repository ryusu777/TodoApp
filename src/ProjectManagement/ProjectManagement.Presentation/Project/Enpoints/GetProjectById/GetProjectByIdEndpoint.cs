using FastEndpoints;
using Library.Models;
using MediatR;
using ProjectManagement.Application.Project.Queries.GetProjectById;

namespace ProjectManagement.Presentation.Project.Endpoints.GetProjectById;

public class GetProjectByIdEndpoint : Endpoint<GetProjectByIdRequest, GetProjectByIdResponse>
{
    private readonly ISender _sender;

    public GetProjectByIdEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(ProjectEndpointRoutes.ProjectDetail);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(GetProjectByIdRequest req, CancellationToken ct)
    {
        var result = await _sender.Send(new GetProjectByIdQuery(req.id));

        if (result.Error != Error.None || result.Value == null)
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetProjectByIdResponse(null, result.Error?.Description ?? "Unknown error")
            ));
            return;
        }

        var project = result.Value;

        await SendOkAsync(new GetProjectByIdResponse(
            new GetProjectByIdResult(
                project.Id,
                project.Name,
                project.Description,
                project.Status,
                project.ProjectPhases,
                project.ProjectMembers
            ), null));
    }
}

