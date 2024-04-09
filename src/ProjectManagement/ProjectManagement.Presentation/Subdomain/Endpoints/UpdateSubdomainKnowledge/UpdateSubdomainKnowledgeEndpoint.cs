using FastEndpoints;
using MediatR;

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
    }

    public override async Task HandleAsync(UpdateSubdomainKnowledgeRequest req, CancellationToken ct)
    {
        if (req.knowledge_id != req.SubdomainKnowledgeId)
        {
            await SendResultAsync(TypedResults.BadRequest());
            return;
        }
        
        var result = await _sender.Send(req);

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new UpdateSubdomainKnowledgeResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
