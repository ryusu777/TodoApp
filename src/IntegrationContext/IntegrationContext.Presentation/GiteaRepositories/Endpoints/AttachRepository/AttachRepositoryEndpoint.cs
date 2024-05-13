using FastEndpoints;
using IntegrationContext.Application.GiteaRepositories.Commands.AttachRepository;
using IntegrationContext.Presentation.Project;
using MediatR;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.AttachRepositoryEndpoint;

public class AttachRepositoryEndpoint : Endpoint<AttachRepositoryRequest, AttachRepositoryResponse>
{
    public ISender _sender;

    public AttachRepositoryEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(GiteaRepositoriesRoutes.AttachRepository);
        Group<GiteaRepositoriesEndpointGroup>();
    }

    public override async Task HandleAsync(AttachRepositoryRequest req, CancellationToken ct)
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

        var result = await _sender.Send(new AttachRepositoryCommand(
            userId, req.ProjectId, req.RepoOwner, req.RepoName
        ), ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new AttachRepositoryResponse(result.Error.Code, result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
