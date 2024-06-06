// create CreateProjectHierarchyEndpoint class
// start
using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProjectHierarchy;
public class CreateProjectHierarchyEndpoint : Endpoint<CreateProjectHierarchyCommand, CreateProjectHierarchyResponse>
{
    private readonly ISender _sender;

    public CreateProjectHierarchyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(ProjectEndpointRoutes.Hierarchies);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(CreateProjectHierarchyCommand request, CancellationToken cancellationToken)
    {
        if (Route<string>("id") != request.ProjectId) {
            await SendResultAsync(TypedResults.BadRequest());

            return;
        }

        var response = await _sender.Send(request, cancellationToken);
        if (response.IsFailure)
            await SendResultAsync(TypedResults.BadRequest(
                new CreateProjectHierarchyResponse(response.Error.Description)
            ));

        await SendNoContentAsync();
    }
}
