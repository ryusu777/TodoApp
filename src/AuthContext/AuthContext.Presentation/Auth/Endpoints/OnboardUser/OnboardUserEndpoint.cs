using AuthContext.Application.User.Commands.OnboardUser;
using FastEndpoints;
using MediatR;

namespace AuthContext.Presentation.Auth.Endpoints.OnboardUser;

public class OnboardUserEndpoint : Endpoint<OnboardUserCommand, OnboardUserResponse>
{
    private readonly ISender _sender;

    public OnboardUserEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post(AuthEndpointRoutes.OnboardUser);
        Group<AuthEndpointGroup>();
    }

    public override async Task HandleAsync(OnboardUserCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsFailure) {
            await SendResultAsync(TypedResults
                .BadRequest(new OnboardUserResponse(result.Error.Description, result.Error.Code)));
            return;
        }

        await SendNoContentAsync();
    }
}

