using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.CreateAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public class CreateAssignmentEndpoint : Endpoint<CreateAssignmentCommand, CreateAssignmentResponse>
{
    public ISender _sender;

    public CreateAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AssignmentEndpointRoutes.Assignments);
        Group<AssignmentEndpointGroup>();
    }

    public override async Task HandleAsync(CreateAssignmentCommand req, CancellationToken ct)
    {
        if (Route<string>("project_id") != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateAssignmentResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
