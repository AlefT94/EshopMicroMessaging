namespace Inventory.API.Bus;

public interface IBusService
{
    Task Publish<T>(T message, CancellationToken cancellationToken);
}
