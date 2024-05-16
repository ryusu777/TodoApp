using MassTransit;
using MassTransitContracts.Data;
using MassTransitContracts.Data.Entities;
using Newtonsoft.Json;

namespace MassTransitContracts.Services;

public class MassTransitService : IMassTransitService
{
    private readonly MassTransitDbContext _dbContext;
    private readonly IBus _bus;

    public MassTransitService(MassTransitDbContext dbContext, IBus bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task PublishEventAsync(object message, CancellationToken ct)
    {
        string messageInJson = JsonConvert.SerializeObject(message, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        var entity = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            EventDetail = messageInJson
        };

        try 
        {
            await _bus.Publish(message, ct);
            entity.PublishedAt = DateTime.Now;
        }
        catch (Exception e)
        {
            entity.Tries++;
            entity.ErrorMessage = e.Message;
        }

        _dbContext.Messages.Add(entity);
        await _dbContext.SaveChangesAsync(ct);
    }
}
