using IntegrationContext.Application.Auth.Commands.GrantAccessToken;
using MassTransit;
using MassTransitContracts.GrantAccessToken;
using MediatR;

namespace IntegrationContext.Application.Auth.Messaging.GetAuthProviderUri;

public class GrantAccessTokenConsumer : IConsumer<GrantAccessTokenRequest>
{
    private readonly ISender _sender;
    public GrantAccessTokenConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<GrantAccessTokenRequest> context)
    {
        var result = await _sender
            .Send(new GrantAccessTokenCommand(context.Message.Code));

        await context.RespondAsync(new GrantAccessTokenResponse());
    }
}

