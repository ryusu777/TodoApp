using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Commands.CreateSubdomain;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomain;

public class CreateSubdomainEndpoint : Endpoint<CreateSubdomainRequest, CreateSubdomainResponse>
{
    public ISender _sender;

    public CreateSubdomainEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(SubdomainEndpointRoutes.Subdomain);
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(CreateSubdomainRequest req, CancellationToken ct)
    {
        if (Route<string>("project_id") != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new CreateSubdomainCommand(
                req.Title, 
                req.Description,
                req.ProjectId));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateSubdomainResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
