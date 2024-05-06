using AuthContext.Application.User.Commands.SignIn;
using FastEndpoints;
using MediatR;

namespace AuthContext.Presentation.Auth.Endpoints.SignIn;

public class SignInEndpoint : Endpoint<SignInCommand, SignInResponse>
{
    private readonly ISender _sender;

    public SignInEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AuthEndpointRoutes.SignIn);
        Group<AuthEndpointGroup>();
    }

    public override async Task HandleAsync(SignInCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsFailure) {
            await SendResultAsync(TypedResults
                .BadRequest(new SignInResponse(result.Error.Description, result.Error.Code, null)));
            return;
        }

        await SendOkAsync(new SignInResponse(null, null, result.Value));
    }
}

