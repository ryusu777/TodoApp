namespace MassTransitContracts.Services;

public interface IMassTransitService
{
    public Task PublishEventAsync(object message, CancellationToken ct);
}
