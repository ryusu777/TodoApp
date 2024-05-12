using AuthContext.Application.User.Commands.RefreshingToken;
using FastEndpoints;
using MediatR;

namespace AuthContext.Presentation.Auth.Endpoints.RefreshToken;

public class RefreshTokenEndpoint : Endpoint<RefreshingTokenCommand, RefreshTokenResponse>
{
    private readonly ISender _sender;

    public RefreshTokenEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AuthEndpointRoutes.RefreshToken);
        Group<AuthEndpointGroup>();
    }

    public override async Task HandleAsync(RefreshingTokenCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsFailure) {
            await SendResultAsync(TypedResults
                .BadRequest(new RefreshTokenResponse(result.Error.Description, result.Error.Code, null)));
            return;
        }

        await SendOkAsync(new RefreshTokenResponse(null, null, result.Value));
    }

}
