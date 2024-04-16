using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Queries.GetSubdomain;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.GetSubdomain;

public class GetSubdomainEndpoint : Endpoint<GetSubdomainRequest, GetSubdomainResponse>
{
    public ISender _sender;

    public GetSubdomainEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(SubdomainEndpointRoutes.SubdomainDetail);
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(GetSubdomainRequest req, CancellationToken ct)
    {
        var result = await _sender
            .Send(new GetSubdomainQuery(
                req.subdomain_id
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetSubdomainResponse(result.Error.Description, null)));
            return;
        }

        await SendOkAsync(new GetSubdomainResponse(
            null,
            result.Value
        ));
    }
}
