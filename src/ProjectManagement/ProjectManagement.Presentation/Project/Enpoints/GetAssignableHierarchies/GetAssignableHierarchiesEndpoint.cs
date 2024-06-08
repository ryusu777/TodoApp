using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Queries.GetAssignableHierarchies;

namespace ProjectManagement.Presentation.Project.Endpoints.GetAssignableHierarchies;

public class GetAssignableHierarchiesEndpoint : Endpoint<GetAssignableHierarchiesQuery, GetAssignableHierarchiesResponse>
{
    private readonly ISender _sender;

    public GetAssignableHierarchiesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(ProjectEndpointRoutes.GetAssignableHierarchies);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(GetAssignableHierarchiesQuery req, CancellationToken ct)
    {
        var user = User.Claims.Where(e => e.Type == "sub").FirstOrDefault();

        req.UserId = user!.Value;

        var result = await _sender.Send(req, ct);

        if (result.IsFailure)
        {
            await SendResultAsync(TypedResults
                .BadRequest(new GetAssignableHierarchiesResponse(result.Error.Description, null)));
            return;
        }
        
        await SendOkAsync(new GetAssignableHierarchiesResponse(null, result.Value));
    }
}   
