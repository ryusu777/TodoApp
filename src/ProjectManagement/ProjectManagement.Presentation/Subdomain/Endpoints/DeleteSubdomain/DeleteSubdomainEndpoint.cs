using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Commands.DeleteSubdomain;

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
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(DeleteSubdomainRequest req, CancellationToken ct)
    {
        var result = await _sender
            .Send(new DeleteSubdomainCommand(
                req.subdomain_id
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteSubdomainResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
