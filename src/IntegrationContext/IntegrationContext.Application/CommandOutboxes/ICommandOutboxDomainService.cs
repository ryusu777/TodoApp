using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Domain.CommandOutboxes;

namespace IntegrationContext.Application.CommandOutboxes;

public interface ICommandOutboxDomainService
{
    public CommandOutbox CreateOutbox(ICommand command);
    public CommandOutbox CreateOutbox<T>(ICommand<T> command);
    public string GetCommandResultInJson(object result);
}
