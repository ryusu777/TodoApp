using FastEndpoints;
using MediatR;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomain;

public class DeleteSubdomainEndpoint : Endpoint<DeleteSubdomainRequest, DeleteSubdomainResponse>
{
    public ISender _sender;

    public DeleteSubdomainEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete(SubdomainEndpointRoutes.SubdomainDetail);
    }

    public override async Task HandleAsync(DeleteSubdomainRequest req, CancellationToken ct)
    {
        if (req.subdomain_id != req.SubdomainId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteSubdomainResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
