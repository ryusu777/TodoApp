using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.UpdateProjectHierarchyMembers;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectHierarchyMembers;
public class UpdateProjectHierarchyMembersEndpoint : Endpoint<UpdateProjectHierarchyMembersCommand, UpdateProjectHierarchyMembersResponse>
{
    private readonly ISender _sender;

    public UpdateProjectHierarchyMembersEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(ProjectEndpointRoutes.HierarchyMembers);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateProjectHierarchyMembersCommand request, CancellationToken cancellationToken)
    {
        if (Route<Guid>("hierarchy-id") != request.HierarchyId || Route<string>("id") != request.ProjectId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var response = await _sender.Send(request, cancellationToken);
        if (response.IsFailure)
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateProjectHierarchyMembersResponse(response.Error.Description)
            ));

        await SendNoContentAsync();
    }
}
