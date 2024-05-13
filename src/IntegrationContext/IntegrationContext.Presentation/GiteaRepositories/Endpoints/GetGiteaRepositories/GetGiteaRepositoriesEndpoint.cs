using FastEndpoints;
using IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;
using IntegrationContext.Presentation.Project;
using MediatR;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.GetGiteaRepositories;

public class GetGiteaRepositoriesEndpoint : Endpoint<GetGiteaRepositoriesRequest, GetGiteaRepositoriesResponse>
{
    public ISender _sender;

    public GetGiteaRepositoriesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(GiteaRepositoriesRoutes.GiteaRepositories);
        Group<GiteaRepositoriesEndpointGroup>();
    }

    public override async Task HandleAsync(GetGiteaRepositoriesRequest req, CancellationToken ct)
    {
        var userId = User
            .Claims
            .FirstOrDefault(e => e.Type == "sub")
            ?.Value;

        if (userId is null)
        {
            await SendForbiddenAsync();
            return;
        }

        var result = await _sender.Send(
            new GetGiteaRepositoryQuery(userId, req.SearchText, req.Page, req.ItemPerPage),
            ct);

        if (result.IsFailure || result.Value is null) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetGiteaRepositoriesResponse(result.Error.Code, result.Error.Description, null)));
            return;
        }

        await SendOkAsync(
            new GetGiteaRepositoriesResponse(null, null, result.Value));
    }
}

