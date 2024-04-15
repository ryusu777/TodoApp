using FastEndpoints;
using MediatR;
using ProjectManagement.Application.Subdomain.Commands.DeleteSubdomainKnowledge;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomainKnowledge;

public class DeleteSubdomainKnowledgeEndpoint : Endpoint<DeleteSubdomainKnowledgeRequest, DeleteSubdomainKnowledgeResponse>
{
    public ISender _sender;

    public DeleteSubdomainKnowledgeEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete(SubdomainEndpointRoutes.DeleteSubdomainKnowledge);
        Group<SubdomainEndpointGroup>();
    }

    public override async Task HandleAsync(DeleteSubdomainKnowledgeRequest req, CancellationToken ct)
    {
        var result = await _sender
            .Send(new DeleteSubdomainKnowledgeCommand(
                req.subdomain_id, 
                req.knowledge_id));

        if (result.IsFailure) 
        {
            await SendResultAsync(TypedResults.BadRequest(
                new DeleteSubdomainKnowledgeResponse(result.Error.Description)));
            return;
        }

        await SendNoContentAsync();
    }
}
