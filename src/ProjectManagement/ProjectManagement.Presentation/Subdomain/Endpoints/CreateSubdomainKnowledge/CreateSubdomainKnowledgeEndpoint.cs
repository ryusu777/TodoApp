using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Commands.CreateSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomainKnowledge;

public class CreateSubdomainKnowledgeEndpoint : Endpoint<CreateSubdomainKnowledgeRequest, CreateSubdomainKnowledgeResponse>
{
    public ISender _sender;

    public CreateSubdomainKnowledgeEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(SubdomainEndpointRoutes.SubdomainDetail);
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(CreateSubdomainKnowledgeRequest req, CancellationToken ct)
    {
        if (Route<Guid>("subdomain_id") != req.SubdomainId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }

        var result = await _sender
            .Send(new CreateSubdomainKnowledgeCommand(
                req.Title,
                req.Content,
                req.SubdomainId
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new CreateSubdomainKnowledgeResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
