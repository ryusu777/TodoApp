// create DeleteProjectHierarchyEndpoint class just like CreateProjectHierarchyEndpoint class
// start

using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.DeleteProjectHierarchy;

namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProjectHierarchy;

public class DeleteProjectHierarchyEndpoint : Endpoint<DeleteProjectHierarchyCommand, DeleteProjectHierarchyResponse>
{
    private readonly ISender _sender;

    public DeleteProjectHierarchyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override async Task HandleAsync(DeleteProjectHierarchyCommand request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        if (response.IsFailure)
            await SendResultAsync(TypedResults
                .BadRequest(new DeleteProjectHierarchyResponse(response.Error.Description)));

        await SendNoContentAsync();
    }
}
