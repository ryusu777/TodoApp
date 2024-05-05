using AuthContext.Application.User.Commands.AuthorizeGitea;
using FastEndpoints;
using MediatR;

namespace AuthContext.Presentation.Auth.Endpoints.AuthorizeGitea;

public class AuthorizeGiteaEndpoint : Endpoint<AuthorizeGiteaCommand, AuthorizeGiteaResponse>
{
    private readonly ISender _sender;

    public AuthorizeGiteaEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AuthEndpointRoutes.AuthorizeGitea);
        Group<AuthEndpointGroup>();
    }

    public override async Task HandleAsync(AuthorizeGiteaCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req);

        if (result.IsFailure || result.Value is null) {
            await SendResultAsync(TypedResults
                .BadRequest(new AuthorizeGiteaResponse(result.Error.Description, result.Error.Code, null)));
            return;
        }

        await SendOkAsync(new AuthorizeGiteaResponse(null, null, result.Value));
    }
}
