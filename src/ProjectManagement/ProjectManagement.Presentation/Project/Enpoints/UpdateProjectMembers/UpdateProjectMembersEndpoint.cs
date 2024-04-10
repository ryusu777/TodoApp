using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.UpdateProjectMembers;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectMembers;

public class UpdateProjectMembersEndpoint : Endpoint<UpdateProjectMembersRequest, UpdateProjectMembersResponse>
{
    public ISender _sender;

    public UpdateProjectMembersEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(ProjectEndpointRoutes.Members);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateProjectMembersRequest req, CancellationToken ct)
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
                new UpdateProjectMembersResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
