using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Commands.UpdateSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomainKnowledge;

public class UpdateSubdomainKnowledgeEndpoint : Endpoint<UpdateSubdomainKnowledgeRequest, UpdateSubdomainKnowledgeResponse>
{
    public ISender _sender;

    public UpdateSubdomainKnowledgeEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put(SubdomainEndpointRoutes.SubdomainKnowledgeDetail);
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(UpdateSubdomainKnowledgeRequest req, CancellationToken ct)
    {
        if (Route<Guid>("knowledge_id") != req.SubdomainKnowledgeId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }
        
        var result = await _sender
            .Send(new UpdateSubdomainKnowledgeCommand(
                req.SubdomainKnowledgeId,
                req.Title,
                req.Content,
                req.SubdomainId
            ));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateSubdomainKnowledgeResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
