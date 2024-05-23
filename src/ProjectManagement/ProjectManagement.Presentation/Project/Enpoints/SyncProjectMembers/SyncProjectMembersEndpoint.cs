using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Project.Commands.SyncProjectMembers;

namespace ProjectManagement.Presentation.Project.Endpoints.SyncProjectMembers;

public class SyncProjectMembersEndpoint : Endpoint<SyncProjectMembersCommand, SyncProjectMembersResponse>
{
    public ISender _sender;

    public SyncProjectMembersEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(ProjectEndpointRoutes.SyncMembers);
        Group<ProjectEndpointGroup>();
    }

    public override async Task HandleAsync(SyncProjectMembersCommand req, CancellationToken ct)
    {
        if (Route<string>("id") != req.ProjectId) {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var username = User.Claims.First(e => e.Type == JwtRegisteredClaimNames.Sub).Value;
        req.UserId = username;
        var result = await _sender.Send(req, ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new SyncProjectMembersResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
