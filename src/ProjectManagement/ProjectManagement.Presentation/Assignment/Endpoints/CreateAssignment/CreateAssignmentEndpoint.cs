using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Assignment.Commands.CreateAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public class CreateAssignmentEndpoint : Endpoint<CreateAssignmentRequest, CreateAssignmentResponse>
{
    public ISender _sender;

    public CreateAssignmentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AssignmentEndpointRoutes.CreateAssignment);
    }

    public override async Task HandleAsync(CreateAssignmentRequest req, CancellationToken ct)
    {
        if (req.project_id != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new CreateAssignmentCommand(
                req.Title, 
                req.Description, 
                req.ProjectId
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateAssignmentResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}