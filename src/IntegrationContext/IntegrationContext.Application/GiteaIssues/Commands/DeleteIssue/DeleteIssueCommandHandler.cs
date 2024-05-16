using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Application.CommandOutboxes.CommandHandler;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.Events;
using Library.Models;

namespace IntegrationContext.Application.GiteaIssues.Commands.DeleteIssue;

public class DeleteIssueCommandHandler : OutboxedCommandHandler<DeleteIssueCommand>
{
    private readonly IGiteaIssueApiService _issueApi;
    private readonly IGiteaUserDomainService _userDomainService;
    public DeleteIssueCommandHandler(
        IUnitOfWork unitOfWork,
        ICommandOutboxDomainService outboxDomainService,
        IGiteaIssueApiService issueApi,
        IGiteaUserDomainService userDomainService) : base(unitOfWork, outboxDomainService)
    {
        _issueApi = issueApi;
        _userDomainService = userDomainService;
    }

    protected override async Task<Result> HandleInternal(
        DeleteIssueCommand request, CancellationToken ct)
    {
        var user = await _userDomainService
            .GetOrRefreshJwt(UserId.Create(request.Message.UserId), ct);

        if (user.IsFailure || user.Value is null || user.Value.JwtToken is null)
            return user.Error;

        var interfaceResult = await _issueApi
            .DeleteIssueAsync(user.Value.JwtToken, request.Message, ct);

        if (interfaceResult.IsFailure || interfaceResult.Value is null)
            return interfaceResult;

        GiteaIssue issue = interfaceResult.Value;
        return await _unitOfWork.SaveChangesAsync(new GiteaIssueDeleted(issue), ct);
    }
}

