using IntegrationContext.Application.GiteaRepositories.Queries.GetAllRepositoryAsignee;
using MassTransit;
using MassTransitContracts.ProjectManagement.Members.GetAllAssignee;
using MediatR;

namespace IntegrationContext.Application.GiteaRepositories.MessageConsumers;

public class GetAllAssigneeConsumer : IConsumer<GetAllAssigneeRequest>
{
    private readonly ISender _sender;

    public GetAllAssigneeConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<GetAllAssigneeRequest> context)
    {
        var result = await _sender
            .Send(new GetAllRepositoryAssigneeQuery(context.Message.ProjectId)
            {
                UserId = context.Message.UserId
            });


        if (result.IsFailure || result.Value is null)
            throw new Exception(result.Error.Description);
                
        await context
            .RespondAsync(
                new GetAllAssigneeResponse(result
                    .Value
                    .Select(e => e.Username)
                    .ToList()));
    }
}

