using IntegrationContext.Application.Auth.Queries.GetAuthProviderUri;
using MassTransit;
using MassTransitContracts.GetAuthProviderUri;
using MediatR;

namespace IntegrationContext.Application.Auth.Messaging.GetAuthProviderUri;

public class GetAuthProviderUriConsumer : IConsumer<GetAuthProviderUriRequest>
{
    private readonly ISender _sender;
    public GetAuthProviderUriConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<GetAuthProviderUriRequest> context)
    {
        var result = await _sender
            .Send(new GetAuthProviderUriQuery(context.Message.State));

        await context.RespondAsync(result.Value!);
    }
}

