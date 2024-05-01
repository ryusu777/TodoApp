using AuthContext.Application.User.Commands.SignInWithGitea;
using FastEndpoints;
using MediatR;

namespace AuthContext.Presentation.User.Endpoints.SignInWithGitea;

public class SignInWithGitea : EndpointWithoutRequest<SignInWithGiteaResponse>
{
    private readonly ISender _sender;

    public SignInWithGitea(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get(UserEndpointRoutes.SignInWithGitea);
        Group<UserEndpointGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _sender.Send(new SignInWithGiteaCommand(), ct);

        if (result.IsFailure || result.Value is null) {
            await SendResultAsync(TypedResults
                .BadRequest(new SignInWithGiteaResponse(result.Error.Description, result.Error.Code, null)));
            return;
        }

        var uri = result.Value!;
        await SendOkAsync(new SignInWithGiteaResponse(null, null, uri.ToString()));
    }
}
