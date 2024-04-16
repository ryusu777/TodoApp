using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Queries.GetSubdomains;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.GetSubdomains;

public class GetSubdomainsEndpoint : Endpoint<GetSubdomainsRequest, GetSubdomainsResponse>
{
    public ISender _sender;

    public GetSubdomainsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(SubdomainEndpointRoutes.Subdomain);
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(GetSubdomainsRequest req, CancellationToken ct)
    {
        var result = await _sender
            .Send(new GetSubdomainsQuery(
                req.project_id
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetSubdomainsResponse(result.Error.Description, null)));
            return;
        }

        await SendOkAsync(new GetSubdomainsResponse(
            null,
            result.Value
        ));
    }
}
