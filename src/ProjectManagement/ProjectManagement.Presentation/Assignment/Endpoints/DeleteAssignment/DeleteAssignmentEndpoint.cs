using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.DeleteAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.DeleteAssignment;

public class DeleteAssignmentEndpoint : Endpoint<DeleteAssignmentRequest, DeleteAssignmentResponse>
{
    public ISender _sender;

    public DeleteAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete(AssignmentEndpointRoutes.DeleteAssignment);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(DeleteAssignmentRequest req, CancellationToken ct)
    {
        var username = User.Claims.First(e => e.Type == JwtRegisteredClaimNames.Sub).Value;
        req.UserId = username;
        var result = await _sender
            .Send(new DeleteAssignmentCommand(req.AssignmentId)
            {
                UserId = username
            }, ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteAssignmentResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
