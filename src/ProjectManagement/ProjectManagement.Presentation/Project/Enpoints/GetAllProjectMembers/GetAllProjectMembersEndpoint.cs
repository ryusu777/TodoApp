using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Queries.GetAllProjectMembers;

namespace ProjectManagement.Presentation.Project.Endpoints.GetAllProjectMembers;

public class GetAllProjectMembersEndpoint : Endpoint<GetAllProjectMembersQuery, GetAllProjectMembersResponse>
{
    private readonly ISender _sender;

    public GetAllProjectMembersEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(ProjectEndpointRoutes.Members);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(GetAllProjectMembersQuery req, CancellationToken ct)
    {
        var result = await _sender.Send(req);

        if (result.IsFailure || result.Value == null)
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetAllProjectMembersResponse(result.Error?.Description ?? "Unknown error", null)
            ));
            return;
        }

        await SendOkAsync(new GetAllProjectMembersResponse(
            null, result.Value));
    }
}
