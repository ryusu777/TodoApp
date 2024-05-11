using FastEndpoints;
using IntegrationContext.Application.GiteaRepositories.Queries.GetProjectRepository;
using IntegrationContext.Presentation.Project;
using MediatR;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.GetProjectRepositories;

public class GetProjectRepositoriesEndpoint : Endpoint<GetProjectRepositoryQuery, GetProjectRepositoriesResponse>
{
    public ISender _sender;

    public GetProjectRepositoriesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(GiteaRepositoriesRoutes.ProjectRepositories);
        Group<GiteaRepositoriesEndpointGroup>();
    }

    public override async Task HandleAsync(GetProjectRepositoryQuery req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsFailure || result.Value is null) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new GetProjectRepositoriesResponse(result.Error.Code, result.Error.Description, null)));
            return;
        }

        await SendOkAsync(
            new GetProjectRepositoriesResponse(null, null, result.Value));
    }
}
