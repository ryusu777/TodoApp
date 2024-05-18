using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.CommandOutboxes;
using IntegrationContext.Domain.CommandOutboxes;
using Newtonsoft.Json;

namespace IntegrationContext.Infrastructure.CommandOutboxes;

public class CommandOutboxDomainService : ICommandOutboxDomainService
{
    public CommandOutbox CreateOutbox(ICommand command)
    {
        string commandInJson = JsonConvert.SerializeObject(command, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        });

        return CommandOutbox.Create(commandInJson);
    }

    public CommandOutbox CreateOutbox<T>(ICommand<T> command)
    {
        string commandInJson = JsonConvert.SerializeObject(command, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        });

        return CommandOutbox.Create(commandInJson);
    }

    public string GetCommandResultInJson(object result)
    {
        string commandInJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        });

        return commandInJson;
    }
}

