using FastEndpoints;
using IntegrationContext.Application.GiteaRepositories.Commands.AttachRepository;
using IntegrationContext.Presentation.Project;
using MediatR;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.AttachRepositoryEndpoint;

public class AttachRepositoryEndpoint : Endpoint<AttachRepositoryCommand, AttachRepositoryResponse>
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

    public override async Task HandleAsync(AttachRepositoryCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new AttachRepositoryResponse(result.Error.Code, result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
