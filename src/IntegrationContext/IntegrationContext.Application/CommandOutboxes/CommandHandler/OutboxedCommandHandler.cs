using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Domain.CommandOutboxes;
using IntegrationContext.Domain.CommandOutboxes.Events;
using Library.Models;

namespace IntegrationContext.Application.CommandOutboxes.CommandHandler;

public abstract class OutboxedCommandHandler<TRequest> : ICommandHandler<TRequest>
    where TRequest : ICommand
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ICommandOutboxDomainService _outboxDomainService;

    public OutboxedCommandHandler(IUnitOfWork unitOfWork, ICommandOutboxDomainService outboxDomainService)
    {
        _unitOfWork = unitOfWork;
        _outboxDomainService = outboxDomainService;
    }

    public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
    {
        CommandOutbox outbox = _outboxDomainService.CreateOutbox(request);
        await _unitOfWork.SaveChangesAsync(new CommandPersisted(outbox), cancellationToken);
        
        var result = await HandleInternal(request, cancellationToken);

        if (result.IsFailure)
        {
            outbox.IncrementTries();
        }

        if (result.IsSuccess)
        {
            outbox.Success(null);
        }

        await _unitOfWork.SaveChangesAsync(outbox.DomainEvents, cancellationToken);

        return result;
    }

    protected abstract Task<Result> HandleInternal(TRequest request, CancellationToken ct);
}

public abstract class OutboxedCommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ICommandOutboxDomainService _outboxDomainService;

    public OutboxedCommandHandler(IUnitOfWork unitOfWork, ICommandOutboxDomainService outboxDomainService)
    {
        _unitOfWork = unitOfWork;
        _outboxDomainService = outboxDomainService;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        CommandOutbox outbox = _outboxDomainService.CreateOutbox(request);
        await _unitOfWork.SaveChangesAsync(new CommandPersisted(outbox), cancellationToken);
        
        var result = await HandleInternal(request, cancellationToken);

        if (result.IsFailure)
        {
            outbox.IncrementTries();
        }

        if (result.IsSuccess)
        {
            outbox.Success(_outboxDomainService.GetCommandResultInJson(result.Value!));
        }

        await _unitOfWork.SaveChangesAsync(outbox.DomainEvents, cancellationToken);

        return result;
    }

    protected abstract Task<Result<TResponse>> HandleInternal(TRequest request, CancellationToken ct);
}
