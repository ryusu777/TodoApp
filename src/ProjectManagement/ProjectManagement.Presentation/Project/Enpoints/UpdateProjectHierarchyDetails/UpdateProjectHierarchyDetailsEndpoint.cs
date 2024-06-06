using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.UpdateProjectHierarchyDetails;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectHierarchyDetails;

public class UpdateProjectHierarchyDetailsEndpoint : Endpoint<UpdateProjectHierarchyDetailsCommand, UpdateProjectHierarchyDetailsResponse>
{
    private readonly ISender _sender;

    public UpdateProjectHierarchyDetailsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override async Task HandleAsync(UpdateProjectHierarchyDetailsCommand request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        if (response.IsFailure)
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateProjectHierarchyDetailsResponse(response.Error.Description)
            ));

        await SendNoContentAsync();
    }
}
