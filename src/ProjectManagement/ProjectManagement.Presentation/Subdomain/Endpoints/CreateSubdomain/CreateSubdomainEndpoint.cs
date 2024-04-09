using FastEndpoints;
using MediatR;

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
    }

    public override async Task HandleAsync(CreateSubdomainRequest req, CancellationToken ct)
    {
        if (req.project_id != req.ProjectId) 
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateSubdomainResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
