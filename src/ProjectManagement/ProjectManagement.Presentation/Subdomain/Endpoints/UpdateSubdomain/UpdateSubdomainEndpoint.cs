using FastEndpoints;
using MediatR;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomain;

public class UpdateSubdomainEndpoint : Endpoint<UpdateSubdomainRequest, UpdateSubdomainResponse>
{
    public ISender _sender;

    public UpdateSubdomainEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(SubdomainEndpointRoutes.SubdomainDetail);
    }

    public override async Task HandleAsync(UpdateSubdomainRequest req, CancellationToken ct)
    {
        if (req.SubdomainId != req.subdomain_id)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateSubdomainResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
